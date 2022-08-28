using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;

namespace GoCode.Application.Common.Validators.Questions
{
    public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
    {
        private const int AnswearsMinimumCount = 2;

        public CreateQuestionDtoValidator(IValidator<CreateAnswearDto> createAnswearValidator)
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Answers)
                .NotEmpty()
                .MustHaveOnlyOneCorrectAnswear()
                .Must(x => x.Count() >= AnswearsMinimumCount)
                .WithMessage(ErrorMessages.Answear.AnswersMinimumCount)
                .ForEach(x => x.SetValidator(createAnswearValidator));

            RuleFor(x => x.Answers.Select(a => a.Content))
                .CollectionMustBeUnique()
                .WithMessage(ErrorMessages.Answear.AnswersMustBeUnique);
        }
    }
}
