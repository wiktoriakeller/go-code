namespace GoCode.Application.Identity.Requests
{
    public record CreateUserRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string? UserName { get; init; }
    }
}
