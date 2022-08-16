namespace GoCode.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public ICollection<Answear> Answers { get; set; }
        public int XP { get; set; }
    }
}
