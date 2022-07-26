﻿using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoCode.Infrastructure.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<TEntity?> GetByIdAsync<TId>(TId id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public IEnumerable<TEntity> GetAll() => _dbContext.Set<TEntity>();

        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate) =>
            _dbContext.Set<TEntity>().Where(predicate);

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<TEntity?> FirstOrDefaultAsyncWith<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> include) =>
            await _dbContext.Set<TEntity>()
            .Include(include)
            .FirstOrDefaultAsync(predicate);

        public async Task<TEntity?> SignleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
