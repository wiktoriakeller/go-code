using FluentValidation;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class UniqueValidationExtension
    {
        public static IRuleBuilderOptions<T, IEnumerable<string>> CollectionMustBeUnique<T>(this IRuleBuilder<T, IEnumerable<string>> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, data, context) =>
            {
                var set = new HashSet<string>();
                foreach (var text in data)
                {
                    if (!set.Contains(text))
                    {
                        set.Add(text);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            });
        }
    }
}
