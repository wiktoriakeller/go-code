using GoCode.Application.Dto.BaseResponse;
using GoCode.Application.Identity.Responses;
using MediatR;

namespace GoCode.Application.Identity.Commands
{
    public class RefreshTokenCommand : IRequest<Response<RefreshTokenResponse>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
