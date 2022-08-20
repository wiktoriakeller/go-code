using GoCode.Application.Common;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Commands
{
    public class CreateCourseCommand : IRequestWrapper<CreateCourseResponse>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<CreateQuestionDto> Questions { get; init; }
    }
}
