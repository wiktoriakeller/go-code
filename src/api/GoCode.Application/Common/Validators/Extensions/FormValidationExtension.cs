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
            return ruleBuilder.MustAsync(async (rootObject, courseId, context) =>
            {
                var userCourse = await userCoursesRepository.GetUserCourseByCourseIdAndUserId(courseId, currentUserService?.User?.Id ?? "");
                if (userCourse is not null)
                {
                    return true;
                }

                return false;
            })
            .WithMessage(ErrorMessages.Form.UserIsNotSignedToThisCourse);
        }
    }
}
