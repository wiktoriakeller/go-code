using GoCode.Application.Common;

namespace GoCode.Application.Identity.Responses
{
    public record RefreshTokenResponse
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}
