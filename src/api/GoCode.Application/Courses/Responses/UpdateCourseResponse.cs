using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Responses
{
    public class UpdateCourseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public int PassPercentTreshold { get; set; }
        public IEnumerable<CreateQuestionDto> Questions { get; set; }
    }
}
