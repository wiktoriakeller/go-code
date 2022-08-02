using GoCode.Application.Contracts.DataAccess;
using GoCode.Infrastructure.Persistence;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GoCode.Infrastructure.DataAccess
{
    public class BaseRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> GetSignleOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
            await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);

        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}