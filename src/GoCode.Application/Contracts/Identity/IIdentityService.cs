using GoCode.Application.Dto.BaseResponse;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;

namespace GoCode.Application.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserCommand createUserCommand);

        Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserCommand authenticateUserCommand);

        Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand);
    }
}
