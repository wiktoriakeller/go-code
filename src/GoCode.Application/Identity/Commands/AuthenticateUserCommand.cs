using GoCode.Application.Dtos.Responses;
using GoCode.Application.Dtos;
using MediatR;

namespace GoCode.Application.Identity.Commands
{
    public class AuthenticateUserCommand : IRequest<Response<AuthenticateUserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}