using System.Linq.Expressions;

namespace GoCode.Application.Common.Contracts.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllWith(Expression<Func<TEntity, bool>> include);

        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetWhereWith(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, bool>> include);

        Task<TEntity?> SignleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task BulkUpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
    }
}
