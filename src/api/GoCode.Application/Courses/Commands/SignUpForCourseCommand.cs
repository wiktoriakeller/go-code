using GoCode.Application.Common;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Commands
{
    public class SignUpForCourseCommand : IRequestWrapper<SignUpForCourseResponse>
    {
        public int Id { get; set; }
    }
}
