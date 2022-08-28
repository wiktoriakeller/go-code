using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Extensions;

namespace GoCode.Application.Common.Validators.Courses
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator(
            IValidator<CreateQuestionDto> createQuestionValidator,
            ICoursesRepository coursesRepository)
        {
            RuleFor(x => x)
                .PropertyMustBeUnique(false, "Name", coursesRepository)
                .WithMessage(ErrorMessages.Course.NameMustBeUnique);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.XP)
                .GreaterThan(0);

            RuleFor(x => x.PassPercentTreshold)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.Questions)
                .NotEmpty()
                .ForEach(q => q.SetValidator(createQuestionValidator));

            RuleFor(x => x.Questions.Select(q => q.Content))
                .CollectionMustBeUnique()
                .WithMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }
    }
}
