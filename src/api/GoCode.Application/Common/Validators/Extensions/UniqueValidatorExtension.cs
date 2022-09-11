using FluentValidation;
using GoCode.Application.Common.Contracts.DataAccess;

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
                    if (set.Contains(text))
                    {
                        return false;
                    }

                    set.Add(text);
                }
                return true;
            });
        }

        public static IRuleBuilderOptions<T, TRequest> PropertyMustBeUnique<T, TRequest, TEntity>(
            this IRuleBuilder<T, TRequest> ruleBuilder,
            bool checkId,
            string propertyName,
            IRepository<TEntity> repository)
            where TEntity : class
        {
            return ruleBuilder.Must((rootObject, current, context) =>
            {
                var allEntities = repository.GetAll();
                int currentId = -1;

                if (checkId)
                {
                    var currentIdProperty = current.GetType().GetProperty("Id");
                    currentId = (int)currentIdProperty.GetValue(current);
                }

                var currentProperty = current.GetType().GetProperty(propertyName);
                var currentPropertyVal = currentProperty.GetValue(current);

                foreach (var entity in allEntities)
                {
                    var id = -1;
                    var property = entity.GetType().GetProperty(propertyName);
                    var propertyVal = property.GetValue(entity);

                    if (checkId)
                    {
                        var idProperty = entity.GetType().GetProperty("Id");
                        id = (int)idProperty.GetValue(entity);
                    }

                    if (propertyVal is not null)
                    {
                        if (checkId && id != currentId && propertyVal.Equals(currentPropertyVal))
                        {
                            return false;
                        }
                        else if (!checkId && propertyVal.Equals(currentPropertyVal))
                        {
                            return false;
                        }
                    }
                }

                return true;
            });
        }
    }
}
