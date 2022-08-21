namespace GoCode.Application.Common.Dtos
{
    public record CourseDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<QuestionDto> Questions { get; init; }
    }
}
