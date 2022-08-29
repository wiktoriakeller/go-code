using GoCode.Application.Common;
using GoCode.Application.Questions.Responses;

namespace GoCode.Application.Questions.Commands
{
    public class DeleteQuestionCommand : IRequestWrapper<DeleteQuestionResponse>
    {
        public int Id { get; set; }
    }
}
