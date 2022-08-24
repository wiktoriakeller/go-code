using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Questions.Responses
{
    public class UpdateQuestionResponse
    {
        public int Id { get; init; }
        public string Content { get; init; }
        public int XP { get; init; }
        public IEnumerable<AnswearDto> Answers { get; init; }
    }
}
