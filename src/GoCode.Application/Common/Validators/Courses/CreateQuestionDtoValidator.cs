using FluentValidation;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;

namespace GoCode.Application.Common.Validators.Courses
{
    public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
    {
        private const string AnswearsLengthError = "At least two answears are required";
        private const string NotUnqiueAsnwearError = "Answers within a question must be unique";

        public CreateQuestionDtoValidator(IValidator<CreateAnswearDto> createAnswearValidator)
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.XP)
                .GreaterThan(0);

            RuleFor(x => x.Answers)
                .NotEmpty()
                .OnlyOneCorrectAnswear()
                .ForEach(x => x.SetValidator(createAnswearValidator));

            RuleFor(x => x.Answers)
                .Must(x => x.Count() >= 2)
                .WithMessage(AnswearsLengthError);

            RuleFor(x => x.Answers.Select(a => a.Content))
                .CollectionMustBeUnique()
                .WithMessage(NotUnqiueAsnwearError);
        }
    }
}
