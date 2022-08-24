using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Questions.Commands;

namespace GoCode.Application.Common.Validators.Questions
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator(ICoursesRepository coursesRepository, IQuestionsRepository questionsRepository,
            IValidator<CreateQuestionDto> questionDtoValidator)
        {
            RuleFor(x => x.Id)
                .EntityWithIdExists(questionsRepository)
                .WithMessage(string.Format(ErrorMessages.NotFound, "Question"));

            RuleFor(x => x)
                .UpdateQuestionCommandContentMustBeUnique(coursesRepository, questionsRepository);

            RuleFor(x => x.Question)
                .SetValidator(questionDtoValidator);
        }
    }
}
