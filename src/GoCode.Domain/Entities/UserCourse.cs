namespace GoCode.Domain.Entities
{
    public class UserCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string UserId { get; set; }
        public uint CourseCompletionPercent { get; set; }
    }
}
