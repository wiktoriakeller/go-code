using GoCode.Application.Courses.Dto;

namespace GoCode.Application.Courses.Responses
{
    public class GetUserCoursesResponse
    {
        public ICollection<CourseDto> Courses { get; init; }
    }
}
