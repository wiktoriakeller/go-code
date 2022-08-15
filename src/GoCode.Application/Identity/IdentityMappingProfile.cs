using AutoMapper;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Requests;

namespace GoCode.Application.Identity
{
    internal class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
            CreateMap<CreateUserRequest, CreateUserCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        }
    }
}
