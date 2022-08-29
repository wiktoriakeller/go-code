using AutoMapper;
using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Questions.Handlers
{
    public class CreateQuestionCommandHandler : IHandlerWrapper<CreateQuestionCommand, CreateQuestionResponse>
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(IQuestionsRepository questionsRepository,
            IMapper mapper)
        {
            _questionsRepository = questionsRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateQuestionResponse>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = _mapper.Map<Question>(request.Question);
            question.CourseId = request.CourseId;
            var addedQuestion = await _questionsRepository.AddAsync(question);
            var response = _mapper.Map<CreateQuestionResponse>(addedQuestion);
            return ResponseResult.Created(response);
        }
    }
}
