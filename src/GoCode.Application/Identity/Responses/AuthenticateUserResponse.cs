namespace GoCode.Application.Identity.Responses
{
    public record AuthenticateUserResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
