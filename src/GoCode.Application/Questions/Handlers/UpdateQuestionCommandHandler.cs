using AutoMapper;
using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Questions.Handlers
{
    public class UpdateQuestionCommandHandler : IHandlerWrapper<UpdateQuestionCommand, UpdateQuestionResponse>
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;

        public UpdateQuestionCommandHandler(IQuestionsRepository questionsRepository,
            IMapper mapper)
        {
            _questionsRepository = questionsRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateQuestionResponse>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionsRepository.GetByIdAsync(request.Id);

            question.Content = request.Question.Content;
            question.Answers = _mapper.Map<ICollection<Answear>>(request.Question.Answers);
            question.XP = request.Question.XP;

            var updatedEntity = await _questionsRepository.UpdateAsync(question);
            var response = _mapper.Map<UpdateQuestionResponse>(updatedEntity);
            return ResponseResult.Updated(response);
        }
    }
}
