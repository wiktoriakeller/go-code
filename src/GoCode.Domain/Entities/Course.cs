namespace GoCode.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
