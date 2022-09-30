using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class FormValidationExtension
    {
        public static IRuleBuilderOptions<T, int> UserMustBeSignedToTheCourse<T>(
            this IRuleBuilder<T, int> ruleBuilder,
            IUserCoursesRepository userCoursesRepository,
            ICurrentUserService currentUserService)
        {
            return ruleBuilder.MustAsync(async (rootObject, id, context) =>
            {
                var userCourse = await userCoursesRepository.GetByIdAsync(id);
                if (userCourse?.UserId == currentUserService?.User?.Id)
                {
                    return true;
                }

                return false;
            })
            .WithMessage(ErrorMessages.Form.UserIsNotSignedToThisCourse);
        }
    }
}
