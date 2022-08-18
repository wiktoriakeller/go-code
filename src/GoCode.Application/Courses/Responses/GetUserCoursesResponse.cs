using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public record GetUserCoursesResponse
    {
        public ICollection<CourseDto> Courses { get; init; }
    }
}
