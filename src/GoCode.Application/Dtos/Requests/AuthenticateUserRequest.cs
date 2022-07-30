namespace GoCode.Application.Dtos.Requests
{
    public record AuthenticateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}