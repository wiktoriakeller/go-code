using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using GoCode.Application.Dtos;

namespace GoCode.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
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
                Token = token
            };

            return new Response<AuthenticateUserResponse>(true, response);
        }
    }
}