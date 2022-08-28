using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public record CreateCourseResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int XP { get; init; }
        public int PassPercentTreshold { get; init; }
        public IEnumerable<CreateQuestionDto> Questions { get; init; }
    }
}
