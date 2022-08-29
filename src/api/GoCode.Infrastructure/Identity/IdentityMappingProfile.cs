using AutoMapper;
using GoCode.Application.Identity.Responses;
using GoCode.Infrastructure.Identity.Entities;

namespace GoCode.Infrastructure.Identity
{
    internal class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<User, CreateUserResponse>();
        }
    }
}
