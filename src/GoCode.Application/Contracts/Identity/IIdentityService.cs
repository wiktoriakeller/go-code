using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Requests;
using GoCode.Application.Dtos.Responses;

namespace GoCode.Application.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest createUserDto);

        Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserRequest authenticateUserRequest);
    }
}