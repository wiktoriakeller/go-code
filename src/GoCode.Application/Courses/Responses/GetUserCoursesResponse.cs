using GoCode.Application.Courses.Dto;

namespace GoCode.Application.Courses.Responses
{
    public record GetUserCoursesResponse
    {
        public ICollection<CourseDto> Courses { get; init; }
    }
}
