using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using GoCode.Application.Dtos;
using System.IdentityModel.Tokens.Jwt;
using GoCode.Application.Contracts.DataAccess;
using System.Security.Claims;

namespace GoCode.Infrastructure.Identity.Entities
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRepositoryAsync<RefreshToken> _refreshTokenRepository;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            IRepositoryAsync<RefreshToken> refreshTokenRepository)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
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

            var token = _jwtService.CreateJwtToken(user);
            var response = new AuthenticateUserResponse
            {
                Token = token,
                RefreshToken = refreshToken
            };

            return new Response<AuthenticateUserResponse>(true, response);
        }

        public async Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand)
        {
            var validatedJwt = _jwtService.GetPrincipalFromToken(refreshTokenCommand.Token);

            if (validatedJwt == null)
            {
                var errors = new List<string> { "Invalid JWT token" };
                return new Response<RefreshTokenResponse>(false, errors);
            }

            var jti = validatedJwt.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var userId = validatedJwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (jti == null || userId == null)
            {
                var errors = new List<string> { "Invalid JWT token" };
                return new Response<RefreshTokenResponse>(false, errors);
            }

            var storedRefreshToken = await _refreshTokenRepository.GetSignleOrDefaultAsync(x => x.Token == refreshTokenCommand.RefreshToken);

            if (storedRefreshToken == null ||
                storedRefreshToken.ExpiryDate >= DateTime.UtcNow ||
                !storedRefreshToken.IsValid ||
                storedRefreshToken.Used ||
                storedRefreshToken.JwtId != jti)
            {
                var errors = new List<string> { "This refresh token does not exist or is invalid" };
                return new Response<RefreshTokenResponse>(false, errors);
            }

            storedRefreshToken.Used = true;
            await _refreshTokenRepository.Update(storedRefreshToken);

            var user = await _userManager.FindByIdAsync(userId);
            return AuthenticateUserAync()
        }
    }
}