using System.Linq.Expressions;

namespace GoCode.Application.Common.Contracts.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity?> GetByIdAsync<TId>(TId id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> SignleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsyncWith<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> include);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task BulkUpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
    }
}
