using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public record GetAllCoursesInfoResponse
    {
        public IEnumerable<CourseInfoDto> Courses { get; init; }
    }
}
