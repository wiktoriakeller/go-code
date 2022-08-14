using AutoMapper;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Identity.Requests
{
    public record AuthenticateUserRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }

    public class AuthenticateUserRequestProfile : Profile
    {
        public AuthenticateUserRequestProfile()
        {
            CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
        }
    }
}
