namespace GoCode.Infrastructure.Identity.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool IsValid { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}