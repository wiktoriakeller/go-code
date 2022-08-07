namespace GoCode.Application.Dtos.Responses
{
    public record AuthenticateUserResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}