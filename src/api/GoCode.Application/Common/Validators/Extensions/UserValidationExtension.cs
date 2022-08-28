using FluentValidation;
using GoCode.Application.Common.Contracts.Identity;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class UserValidationExtension
    {
        public static IRuleBuilderOptions<T, string> EmailIsPresent<T>(this IRuleBuilder<T, string> ruleBuilder, bool isPresent,
            IIdentityService identityService)
        {
            return ruleBuilder.MustAsync(async (rootObject, email, context) =>
            {
                var response = await identityService.GetUserByEmail(email);
                if ((isPresent && response.Succeeded) || (!isPresent && !response.Succeeded))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
