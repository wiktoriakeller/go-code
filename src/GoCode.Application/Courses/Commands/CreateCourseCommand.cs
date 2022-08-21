using GoCode.Application.Common;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Commands
{
    public class CreateCourseCommand : IRequestWrapper<CreateCourseResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateQuestionDto> Questions { get; set; }
    }
}
