using FluentValidation;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Common.Validators.Extensions;

namespace GoCode.Application.Common.Validators.Courses
{
    public class SignUpForCourseCommandValidator : AbstractValidator<SignUpForCourseCommand>
    {
        public SignUpForCourseCommandValidator(ICoursesRepository coursesRepository)
        {
            RuleFor(x => x.Id)
                .EntityWithIdExists(coursesRepository);
        }
    }
}
