namespace GoCode.Application.Common.Dtos
{
    public record UserCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public string Description { get; set; }
        public int PassPercentTreshold { get; set; }
        public bool UserPassed { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
