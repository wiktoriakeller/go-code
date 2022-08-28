using FluentValidation;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Commands;

namespace GoCode.Application.Common.Validators.Courses
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator(IValidator<CreateCourseDto> courseValidator)
        {
            RuleFor(x => x.Course)
                .SetValidator(courseValidator);
        }
    }
}
