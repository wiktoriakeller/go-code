using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Questions.Requests
{
    public record UpdateQuestionRequest
    {
        public string Content { get; set; }
        public int XP { get; set; }
        public IEnumerable<CreateAnswearDto> Answers { get; set; }
    }
}
