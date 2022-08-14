using AutoMapper;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Identity.Requests
{
    public record CreateUserRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    public class CreateUserRequestProfile : Profile
    {
        public CreateUserRequestProfile()
        {
            CreateMap<CreateUserRequest, CreateUserCommand>();
        }
    }
}
