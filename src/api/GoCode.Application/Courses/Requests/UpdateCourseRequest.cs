using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Courses.Requests
{
    public class UpdateCourseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public int PassPercentTreshold { get; set; }
        public IEnumerable<CreateQuestionDto> Questions { get; set; }
    }
}
