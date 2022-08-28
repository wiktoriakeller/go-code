namespace GoCode.Application.Identity.Requests
{
    public record RefreshTokenRequest
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}
