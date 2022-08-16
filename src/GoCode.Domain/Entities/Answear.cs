namespace GoCode.Domain.Entities
{
    public class Answear : BaseEntity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
