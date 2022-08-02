using System.Linq.Expressions;

namespace GoCode.Application.Contracts.DataAccess
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T?> GetSignleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task Update(T entity);

        Task Delete(T entity);
    }
}