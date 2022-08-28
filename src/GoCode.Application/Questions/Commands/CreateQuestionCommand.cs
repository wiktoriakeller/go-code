using GoCode.Application.Common;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Questions.Responses;

namespace GoCode.Application.Questions.Commands
{
    public class CreateQuestionCommand : IRequestWrapper<CreateQuestionResponse>
    {
        public int CourseId { get; set; }
        public CreateQuestionDto Question { get; set; }
    }
}
