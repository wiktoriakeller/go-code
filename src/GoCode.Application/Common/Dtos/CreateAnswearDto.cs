namespace GoCode.Application.Common.Dtos
{
    public record CreateAnswearDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
