using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Requests
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CreateQuestionDto> Questions { get; set; }
    }
}
