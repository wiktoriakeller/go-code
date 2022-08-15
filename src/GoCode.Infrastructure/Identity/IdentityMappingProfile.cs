using AutoMapper;
using GoCode.Application.Dto.Identity;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;
using GoCode.Infrastructure.Identity.Entities;

namespace GoCode.Infrastructure.Identity
{
    internal class IdentityMappingProfile : Profile
    {
        internal IdentityMappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<CreateUserCommand, ApplicationUser>();
            CreateMap<ApplicationUser, CreateUserResponse>();
        }
    }
}
