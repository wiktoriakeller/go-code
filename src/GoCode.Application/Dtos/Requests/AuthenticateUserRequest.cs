namespace GoCode.Application.Dtos.Requests
{
    public record AuthenticateUserRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}