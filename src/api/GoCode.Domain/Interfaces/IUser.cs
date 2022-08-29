using GoCode.Domain.Entities;

namespace GoCode.Domain.Interfaces
{
    public interface IUser
    {
        public string Id { get; }
        public string UserName { get; }
        public int TotalXP { get; }
        public int Level { get; }

        public ICollection<UserCourse> UserCourses { get; }
    }
}
