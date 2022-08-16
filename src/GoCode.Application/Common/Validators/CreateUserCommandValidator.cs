using FluentValidation;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Common.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private const string UserExists = "User with that email already exists";

        public CreateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Email)
                .EmailIsPresent(false, identityService)
                .WithMessage(UserExists);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
}
