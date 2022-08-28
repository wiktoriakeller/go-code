using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Validators.Extensions;
using GoCode.Application.Identity.Commands;

namespace GoCode.Application.Common.Validators.Identity
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .EmailIsPresent(false, identityService)
                .WithMessage(ErrorMessages.Identity.UserExists);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
}
