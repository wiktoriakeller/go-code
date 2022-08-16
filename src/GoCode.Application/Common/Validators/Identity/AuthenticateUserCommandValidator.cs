using FluentValidation;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Common.Validators.Identity
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        private const string EmailNotFound = "Email was not found";

        public AuthenticateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Email)
                .EmailIsPresent(true, identityService)
                .WithMessage(EmailNotFound);

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
