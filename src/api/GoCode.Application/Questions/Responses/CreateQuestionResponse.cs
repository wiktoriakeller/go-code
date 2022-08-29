using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Questions.Responses
{
    public record CreateQuestionResponse
    {
        public int Id { get; init; }
        public string Content { get; init; }
        public IEnumerable<AnswearDto> Answers { get; init; }
    }
}
