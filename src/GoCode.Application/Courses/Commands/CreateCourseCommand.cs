using GoCode.Application.Common;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Commands
{
    public class CreateCourseCommand : IRequestWrapper<CreateCourseResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
