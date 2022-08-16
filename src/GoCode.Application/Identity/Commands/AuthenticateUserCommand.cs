using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Identity.Responses;
using MediatR;

namespace GoCode.Application.Identity.Commands
{
    public class AuthenticateUserCommand : IRequest<Response<AuthenticateUserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
