namespace GoCode.Application.Common.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Content { get; init; }
        public int XP { get; init; }
        public IEnumerable<AnswearDto> Answers { get; init; }
    }
}
