using GoCode.Domain.Entities;

namespace GoCode.Domain.Interfaces
{
    public interface IUser
    {
        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public uint TotalXP { get; }
        public uint Level { get; }

        public ICollection<UserCourse> UserCourses { get; }
    }
}
