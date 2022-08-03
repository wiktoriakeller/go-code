using GoCode.Application.Contracts.DataAccess;
using GoCode.Infrastructure.Persistence;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GoCode.Infrastructure.DataAccess
{
    public class BaseRepositoryAsync<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) =>
            await _dbContext.Set<T>().Where(predicate).ToListAsync();

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
            await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T?> SignleOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
            await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);

        public async Task AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BulkUpdateAsync(IEnumerable<T> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}