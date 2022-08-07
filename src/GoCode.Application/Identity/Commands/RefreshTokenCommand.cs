using GoCode.Application.Dtos.Responses;
using GoCode.Application.Dtos;
using MediatR;

namespace GoCode.Application.Identity.Commands
{
    public class RefreshTokenCommand : IRequest<Response<RefreshTokenResponse>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}