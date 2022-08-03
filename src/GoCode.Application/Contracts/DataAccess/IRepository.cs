using System.Linq.Expressions;

namespace GoCode.Application.Contracts.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        Task<T?> SignleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task BulkUpdateAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);
    }
}