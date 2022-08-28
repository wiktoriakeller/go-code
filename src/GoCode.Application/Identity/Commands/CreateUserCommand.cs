using GoCode.Application.Common;
using GoCode.Application.Identity.Responses;

namespace GoCode.Application.Identity.Commands
{
    public class CreateUserCommand : IRequestWrapper<CreateUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserName { get; set; }
    }
}
