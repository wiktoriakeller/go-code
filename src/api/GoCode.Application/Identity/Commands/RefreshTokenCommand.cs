using GoCode.Application.Common;
using GoCode.Application.Identity.Responses;

namespace GoCode.Application.Identity.Commands
{
    public class RefreshTokenCommand : IRequestWrapper<RefreshTokenResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
