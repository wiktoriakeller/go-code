using GoCode.Application.Dtos.Responses;
using GoCode.Application.Dtos;
using MediatR;

namespace GoCode.Application.Identity.Commands
{
    public class CreateUserCommand : IRequest<Response<CreateUserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}