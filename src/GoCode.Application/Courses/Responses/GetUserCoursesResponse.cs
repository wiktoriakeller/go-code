using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public record GetUserCoursesResponse
    {
        public IEnumerable<CourseDto> Courses { get; init; }
    }
}
