using GoCode.Application.Common;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Commands
{
    public class DeleteCourseCommand : IRequestWrapper<DeleteCourseResponse>
    {
        public int Id { get; set; }
    }
}
