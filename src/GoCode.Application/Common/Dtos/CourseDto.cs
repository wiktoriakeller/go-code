namespace GoCode.Application.Common.Dtos
{
    public record CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
