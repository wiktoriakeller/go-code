using FluentValidation;
using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Common.Validators.Answers
{
    public class CreateAnswearDtoValidator : AbstractValidator<CreateAnswearDto>
    {
        public CreateAnswearDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
