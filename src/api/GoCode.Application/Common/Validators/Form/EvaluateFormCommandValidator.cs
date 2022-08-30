using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Form.Commands;

namespace GoCode.Application.Common.Validators.Form
{
    public class EvaluateFormCommandValidator : AbstractValidator<EvaluateFormCommand>
    {
        public EvaluateFormCommandValidator(
            ICoursesRepository coursesRepository,
            IUserCoursesRepository userCoursesRepository,
            ICurrentUserService currentUserService)
        {
            RuleFor(x => x.CourseId)
                .EntityWithIdMustExist(coursesRepository)
                .WithMessage(string.Format(ErrorMessages.NotFound, "Course"))
                .UserMustBeSignedToTheCourse(userCoursesRepository, currentUserService);
        }
    }
}
