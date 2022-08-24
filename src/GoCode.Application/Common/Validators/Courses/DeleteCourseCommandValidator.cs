using FluentValidation;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Common.Contracts.DataAccess;

namespace GoCode.Application.Common.Validators.Courses
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator(ICoursesRepository coursesRepository)
        {
            RuleFor(x => x.Id)
                .EntityWithIdExists(coursesRepository);
        }
    }
}
