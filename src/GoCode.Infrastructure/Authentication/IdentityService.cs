using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Requests;
using GoCode.Application.Dtos.Responses;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoCode.Infrastructure.Authentication
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

        public async Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var newUser = new ApplicationUser
            {
                Email = createUserRequest.Email,
                UserName = createUserRequest.Email,
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName
            };

            var result = await _userManager.CreateAsync(newUser, createUserRequest.Password);

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

        public async Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserRequest authenticateUserRequest)
        {
            var user = await _userManager.FindByEmailAsync(authenticateUserRequest.Email);

            if (user == null)
            {
                var errors = new List<string> { "Incorrect credentials" };
                return new Response<AuthenticateUserResponse>(false, errors);
            }

            var userPasswordIsValid = await _userManager.CheckPasswordAsync(user, authenticateUserRequest.Password);

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