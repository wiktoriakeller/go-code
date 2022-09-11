using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public record GetAllCoursesResponse
    {
        public IEnumerable<CourseDto> Courses { get; init; }
    }
}
