using System.Linq.Expressions;

namespace GoCode.Application.Contracts.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> SignleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task Add(T entity);

        Task Update(T entity);

        Task BulkUpdate(IEnumerable<T> entities);

        Task Delete(T entity);
    }
}