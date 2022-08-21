using FluentValidation;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Courses.Commands;

namespace GoCode.Application.Common.Validators.Courses
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        private const string NotUniqueQuestionError = "Questions within a course must be unique";

        public CreateCourseCommandValidator(IValidator<CreateQuestionDto> createQuestionValidator)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Questions)
                .NotEmpty()
                .ForEach(q => q.SetValidator(createQuestionValidator));

            RuleFor(x => x.Questions.Select(q => q.Content))
                .CollectionMustBeUnique()
                .WithMessage(NotUniqueQuestionError);
        }
    }
}
