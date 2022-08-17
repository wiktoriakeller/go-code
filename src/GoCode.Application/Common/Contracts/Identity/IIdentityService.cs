using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;
using GoCode.Domain.Interfaces;

namespace GoCode.Application.Common.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<Response<IUser>> GetUserByEmail(string email);

        Task<Response<IUser>> GetUserFromTokenAsync(string? token);

        Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserCommand createUserCommand);

        Task<Response<AuthenticateUserResponse>> AuthenticateUserAync(AuthenticateUserCommand authenticateUserCommand);

        Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand);
    }
}
