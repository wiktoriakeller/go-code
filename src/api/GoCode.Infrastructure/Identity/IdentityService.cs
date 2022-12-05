using AutoMapper;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Services;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;
using GoCode.Domain.Interfaces;
using GoCode.Infrastructure.Identity.Entities;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GoCode.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(UserManager<User> userManager,
            IJwtService jwtService,
            IRepository<RefreshToken> refreshTokenRepository,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider,
            IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Response<IUser>> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return ResponseResult.NotFound<IUser>(ErrorMessages.Identity.UserNotFound);
            }

            return ResponseResult.Ok<IUser>(user);
        }

        public async Task<Response<IUser>> GetUserFromTokenAsync(string? token)
        {
            var result = IsJwtTokenValid<IUser>(token, true);

            if (!result.response.Succeeded)
            {
                return result.response;
            }

            var user = await _userManager.FindByIdAsync(result.userId);

            if (user == null)
            {
                return ResponseResult.NotFound<IUser>(ErrorMessages.Identity.UserNotFound);
            }

            return ResponseResult.Ok<IUser>(user);
        }

        public async Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserCommand createUserCommand)
        {
            var newUser = new User
            {
                Email = createUserCommand.Email,
                UserName = createUserCommand.UserName ?? createUserCommand.Email
            };

            var result = await _userManager.CreateAsync(newUser, createUserCommand.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return ResponseResult.ValidationError<CreateUserResponse>(errors);
            }

            var response = _mapper.Map<CreateUserResponse>(newUser);
            return ResponseResult.Ok(response);
        }

        public async Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserCommand authenticateUserCommand)
        {
            var user = await _userManager.FindByEmailAsync(authenticateUserCommand.Email);

            if (user == null)
            {
                return ResponseResult.ValidationError<AuthenticateUserResponse>(ErrorMessages.Identity.IncorrectCredentials);
            }

            var userPasswordIsValid = await _userManager.CheckPasswordAsync(user, authenticateUserCommand.Password);

            if (!userPasswordIsValid)
            {
                return ResponseResult.ValidationError<AuthenticateUserResponse>(ErrorMessages.Identity.IncorrectCredentials);
            }

            var newJwt = await _jwtService.CreateJwtToken(user);
            var newRefreshToken = await CreateRefreshToken(newJwt.Jti, user);

            var response = new AuthenticateUserResponse
            {
                Token = newJwt.Token,
                RefreshToken = newRefreshToken
            };

            return ResponseResult.Ok(response);
        }

        public async Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand)
        {
            var result = IsJwtTokenValid<RefreshTokenResponse>(refreshTokenCommand.Token, false);

            if (!result.response.Succeeded)
            {
                return result.response;
            }

            var refreshTokens = _refreshTokenRepository
                .GetWhere(x => x.Token == refreshTokenCommand.RefreshToken)
                .ToList();

            if (refreshTokens.Count != 1)
            {
                return ResponseResult.Unauthorized<RefreshTokenResponse>(ErrorMessages.Identity.InvalidToken);
            }

            var currentRefreshToken = refreshTokens.First();

            if (currentRefreshToken.ExpiryDate >= DateTime.UtcNow || currentRefreshToken.JwtId != result.jti)
            {
                return ResponseResult.Unauthorized<RefreshTokenResponse>(ErrorMessages.Identity.InvalidToken);
            }

            var user = await _userManager.FindByIdAsync(result.userId);
            var newJwt = await _jwtService.CreateJwtToken(user);
            var newRefreshToken = await CreateRefreshToken(newJwt.Jti, user);

            var response = new RefreshTokenResponse
            {
                Token = newJwt.Token,
                RefreshToken = newRefreshToken
            };

            return ResponseResult.Ok(response);
        }

        private (Response<T> response, string? userId, string? jti) IsJwtTokenValid<T>(string? token, bool validateLifetime)
        {
            if (token is null)
            {
                return (ResponseResult.Unauthorized<T>(ErrorMessages.Identity.InvalidToken), null, null);
            }

            var validatedJwt = _jwtService.GetPrincipalFromJwtToken(token, validateLifetime);

            if (validatedJwt == null)
            {
                return (ResponseResult.Unauthorized<T>(ErrorMessages.Identity.InvalidToken), null, null);
            }

            var jti = validatedJwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var userId = validatedJwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (jti == null || userId == null)
            {
                return (ResponseResult.Unauthorized<T>(ErrorMessages.Identity.InvalidToken), null, null);
            }

            return (ResponseResult.Ok<T>(), userId, jti);
        }

        private async Task<string> CreateRefreshToken(string jti, User user)
        {
            var currentRefresh = await _refreshTokenRepository.FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (currentRefresh != null)
            {
                await _refreshTokenRepository.DeleteAsync(currentRefresh);
            }

            var refreshToken = new RefreshToken
            {
                Token = GetUniqueToken(),
                JwtId = jti,
                CreationDate = _dateTimeProvider.UtcNow,
                ExpiryDate = _dateTimeProvider.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays),
                UserId = user.Id,
                User = user
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken.Token;
        }

        private string GetUniqueToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var tokenIsUsed = _userManager.Users.Any(u => u.RefreshToken != null && u.RefreshToken.Token == token);

            if (tokenIsUsed)
            {
                return GetUniqueToken();
            }

            return token;
        }
    }
}
