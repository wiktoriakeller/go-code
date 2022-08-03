using GoCode.Application.Contracts.DataAccess;
using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GoCode.Infrastructure.Identity.Entities
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            IRepository<RefreshToken> refreshTokenRepository,
            IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserCommand createUserCommand)
        {
            var newUser = new ApplicationUser
            {
                Email = createUserCommand.Email,
                UserName = createUserCommand.Email,
                FirstName = createUserCommand.FirstName,
                LastName = createUserCommand.LastName
            };

            var result = await _userManager.CreateAsync(newUser, createUserCommand.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new Response<CreateUserResponse>(false, errors);
            }

            var response = new CreateUserResponse
            {
                UserId = newUser.Id
            };

            return new Response<CreateUserResponse>(true, response);
        }

        public async Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserCommand authenticateUserCommand)
        {
            var user = await _userManager.FindByEmailAsync(authenticateUserCommand.Email);

            if (user == null)
            {
                var errors = new List<string> { "Incorrect credentials" };
                return new Response<AuthenticateUserResponse>(false, errors);
            }

            var userPasswordIsValid = await _userManager.CheckPasswordAsync(user, authenticateUserCommand.Password);

            if (!userPasswordIsValid)
            {
                var errors = new List<string> { "Incorrect credentials" };
                return new Response<AuthenticateUserResponse>(false, errors);
            }

            var (jwtToken, jti) = _jwtService.CreateJwtToken(user);
            var refreshToken = await CreateRefreshToken(jti, user);

            var response = new AuthenticateUserResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };

            return new Response<AuthenticateUserResponse>(true, response);
        }

        public async Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand)
        {
            var validatedJwt = _jwtService.GetPrincipalFromJwtToken(refreshTokenCommand.Token);

            if (validatedJwt == null)
            {
                var errors = new List<string> { "Invalid JWT token" };
                return new Response<RefreshTokenResponse>(false, errors);
            }

            var jti = validatedJwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var userId = validatedJwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (jti == null || userId == null)
            {
                var errors = new List<string> { "Invalid JWT token" };
                return new Response<RefreshTokenResponse>(false, errors);
            }

            var storedRefreshToken = await _refreshTokenRepository.SignleOrDefaultAsync(x => x.Token == refreshTokenCommand.RefreshToken);

            if (storedRefreshToken == null ||
                storedRefreshToken.ExpiryDate >= DateTime.UtcNow ||
                storedRefreshToken.JwtId != jti)
            {
                var errors = new List<string> { "This refresh token does not exist or is invalid" };
                return new Response<RefreshTokenResponse>(false, errors);
            }

            var user = await _userManager.FindByIdAsync(userId);
            var (jwtToken, newJti) = _jwtService.CreateJwtToken(user);
            var refreshToken = await CreateRefreshToken(newJti, user);

            var result = new RefreshTokenResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };

            return new Response<RefreshTokenResponse>(true, result);
        }

        private async Task<string> CreateRefreshToken(string jti, ApplicationUser user)
        {
            if (user.RefreshToken != null)
            {
                await _refreshTokenRepository.Delete(user.RefreshToken);
            }

            var refreshToken = new RefreshToken
            {
                Token = GetUniqueToken(),
                JwtId = jti,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays),
                UserId = user.Id,
                User = user
            };

            await _refreshTokenRepository.Add(refreshToken);
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