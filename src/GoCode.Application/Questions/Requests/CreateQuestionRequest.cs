using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Questions.Requests
{
    public record CreateQuestionRequest
    {
        public int CourseId { get; init; }
        public CreateQuestionDto Question { get; init; }
    }
}
