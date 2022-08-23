using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Responses;

namespace GoCode.Application.Questions.Handlers
{
    public class DeleteQuestionCommandHandler : IHandlerWrapper<DeleteQuestionCommand, DeleteQuestionResponse>
    {
        private readonly IQuestionsRepository _questionsRepository;

        public DeleteQuestionCommandHandler(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<Response<DeleteQuestionResponse>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionsRepository.GetByIdAsync(request.Id);
            await _questionsRepository.DeleteAsync(question);
            var response = new DeleteQuestionResponse() { Id = request.Id };
            return ResponseResult.Ok(response);
            return ResponseResult.Ok(response);
        }
    }
}
