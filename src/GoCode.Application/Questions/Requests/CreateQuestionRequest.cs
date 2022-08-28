using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Questions.Requests
{
    public record CreateQuestionRequest
    {
        public int CourseId { get; init; }
        public string Content { get; set; }
        public IEnumerable<CreateAnswearDto> Answers { get; set; }
    }
}
