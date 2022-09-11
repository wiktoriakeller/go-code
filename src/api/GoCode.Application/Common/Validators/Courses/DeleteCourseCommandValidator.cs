using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Courses.Commands;

namespace GoCode.Application.Common.Validators.Courses
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator(ICoursesRepository coursesRepository)
        {
            RuleFor(x => x.Id)
                .EntityWithIdMustExist(coursesRepository)
                .WithMessage(string.Format(ErrorMessages.NotFound, "Course"));
        }
    }
}
