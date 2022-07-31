using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserCommand createUserCommand);

        Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserCommand authenticateUserCommand);
    }
}