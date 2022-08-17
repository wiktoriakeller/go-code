using GoCode.Domain.Entities;
using GoCode.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoCode.Infrastructure.Identity.Entities
{
    public class User : IdentityUser, IUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public uint TotalXP { get; set; }
        public uint Level { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
