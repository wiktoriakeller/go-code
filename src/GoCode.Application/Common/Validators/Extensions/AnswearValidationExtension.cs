using FluentValidation;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class AnswearValidationExtension
    {
        public static IRuleBuilderOptions<T, IEnumerable<CreateAnswearDto>> OnlyOneCorrectAnswear<T>(this IRuleBuilder<T, IEnumerable<CreateAnswearDto>> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, answers, context) =>
            {
                var foundTrue = false;
                foreach (var answer in answers)
                {
                    if (answer.IsCorrect && !foundTrue)
                    {
                        foundTrue = true;
                    }
                    else if (answer.IsCorrect && foundTrue)
                    {
                        return false;
                    }
                }

                return true;
            })
            .WithMessage(ErrorMessages.Answear.OnlyOneAnswarCanBeCorrect);
        }
    }
}
