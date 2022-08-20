namespace GoCode.Application.Common.Dtos
{
    public record CreateAnswearDto
    {
        public string Content { get; init; }
        public bool IsCorrect { get; init; }
    }
}
