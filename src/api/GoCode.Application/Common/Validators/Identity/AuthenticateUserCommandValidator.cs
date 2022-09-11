using FluentValidation;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Common.Validators.Identity
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        private const string UserWithEmailNotFound = "User with that email was not found";

        public AuthenticateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .EmailIsPresent(true, identityService)
                .WithMessage(UserWithEmailNotFound);

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
