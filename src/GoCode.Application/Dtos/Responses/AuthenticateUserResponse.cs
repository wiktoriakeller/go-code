namespace GoCode.Application.Dtos.Responses
{
    public record AuthenticateUserResponse
    {
        public string Token { get; set; }
    }
}