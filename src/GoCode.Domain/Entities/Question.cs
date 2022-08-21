namespace GoCode.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int XP { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Answear> Answers { get; set; }
    }
}
