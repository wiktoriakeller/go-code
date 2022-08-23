using FluentValidation;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Questions.Commands;

namespace GoCode.Application.Common.Validators.Questions
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator(IValidator<CreateQuestionDto> questionDtoValidator,
            ICoursesRepository coursesRepository)
        {
            RuleFor(x => x.CourseId)
                .EntityWithIdExists(coursesRepository);

            RuleFor(x => x)
                .QuestionContentMustBeUnique(coursesRepository);

            RuleFor(x => x.Question)
                .SetValidator(questionDtoValidator);
        }
    }
}
