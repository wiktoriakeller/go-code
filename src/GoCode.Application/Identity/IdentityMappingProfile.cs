using AutoMapper;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Requests;

namespace GoCode.Application.Identity
{
    internal class CoursesMappingProfile : Profile
    {
        public CoursesMappingProfile()
        {
            CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
            CreateMap<CreateUserRequest, CreateUserCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        }
    }
}
