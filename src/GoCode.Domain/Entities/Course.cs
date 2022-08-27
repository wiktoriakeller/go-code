namespace GoCode.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public int PassPercentTreshold { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
