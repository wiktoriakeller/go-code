namespace GoCode.Application.Common.Dtos
{
    public record CreateQuestionDto
    {
        public string Content { get; set; }
        public int XP { get; set; }
        public ICollection<CreateAnswearDto> Answers { get; set; }
    }
}
