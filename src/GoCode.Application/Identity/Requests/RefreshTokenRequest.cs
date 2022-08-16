namespace GoCode.Application.Identity.Requests
{
    public record RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
