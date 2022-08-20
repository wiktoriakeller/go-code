using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Requests
{
    public class CreateCourseRequest
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<CreateQuestionDto> Questions { get; init; }
    }
}
