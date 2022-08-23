using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Questions.Commands;

namespace GoCode.Application.Common.Validators.Questions
{
    public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator(IQuestionsRepository questionsRepository)
        {
            RuleFor(x => x.Id)
                .EntityWithIdExists(questionsRepository)
                .WithMessage(string.Format(ErrorMessages.NotFound, "Question"));
        }
    }
}
