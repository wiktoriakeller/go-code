namespace GoCode.Application.Common.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<AnswearDto> Answers { get; set; }
    }
}
