using AutoMapper;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Identity.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenRequestProfile : Profile
    {
        public RefreshTokenRequestProfile()
        {
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        }
    }
}
