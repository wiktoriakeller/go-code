using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Common.Validators.Identity
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .EmailIsPresent(true, identityService)
                .WithMessage(string.Format(ErrorMessages.NotFound, "Email"));

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
