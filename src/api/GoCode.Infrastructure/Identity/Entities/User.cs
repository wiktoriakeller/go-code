using GoCode.Domain.Entities;
using GoCode.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoCode.Infrastructure.Identity.Entities
{
    public class User : IdentityUser, IUser
    {
        public int TotalXP { get; set; }
        public int Level { get; set; } = 1;
        public ICollection<UserCourse> UserCourses { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
