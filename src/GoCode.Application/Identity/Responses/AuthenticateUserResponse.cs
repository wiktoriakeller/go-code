namespace GoCode.Application.Identity.Responses
{
    public record AuthenticateUserResponse
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}
