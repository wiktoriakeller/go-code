using GoCode.Application.Common;
using GoCode.Application.Identity.Responses;

namespace GoCode.Application.Identity.Commands
{
    public class AuthenticateUserCommand : IRequestWrapper<AuthenticateUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
