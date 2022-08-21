using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Requests
{
    public record CreateCourseRequest
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public IEnumerable<CreateQuestionDto> Questions { get; init; }
    }
}
