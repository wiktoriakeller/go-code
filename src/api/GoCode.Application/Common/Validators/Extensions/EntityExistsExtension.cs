using FluentValidation;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;

namespace GoCode.Application.Common.Validators.Extensions
{
    public static class EntityExistsExtension
    {
        public static IRuleBuilderOptions<T, int> EntityWithIdMustExist<T, TEntity>(this IRuleBuilder<T, int> ruleBuilder,
            IRepository<TEntity> repository) where TEntity : class
        {
            return ruleBuilder.MustAsync(async (rootObject, id, context) =>
            {
                var entity = await repository.GetByIdAsync(id);
                if (entity is not null)
                {
                    return true;
                }

                return false;
            })
            .WithMessage(string.Format(ErrorMessages.NotFound, "Entity"))
            .WithErrorCode(ResponseError.NotFound.ToString());
        }
    }
}
