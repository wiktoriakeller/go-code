namespace GoCode.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
