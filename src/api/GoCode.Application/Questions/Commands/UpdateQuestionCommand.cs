using GoCode.Application.Common;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Questions.Responses;

namespace GoCode.Application.Questions.Commands
{
    public class UpdateQuestionCommand : IRequestWrapper<UpdateQuestionResponse>
    {
        public int Id { get; set; }
        public CreateQuestionDto Question { get; set; }
    }
}
