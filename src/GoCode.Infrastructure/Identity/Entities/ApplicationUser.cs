using Microsoft.AspNetCore.Identity;

namespace GoCode.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}