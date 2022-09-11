using GoCode.Application.Common;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Responses;

namespace GoCode.Application.Courses.Commands
{
    public class UpdateCourseCommand : IRequestWrapper<UpdateCourseResponse>
    {
        public int Id { get; set; }
        public CreateCourseDto Course { get; set; }
    }
}
