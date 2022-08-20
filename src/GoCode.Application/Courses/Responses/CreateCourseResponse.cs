using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public record CreateCourseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CreateQuestionDto> Questions { get; set; }
    }
}
