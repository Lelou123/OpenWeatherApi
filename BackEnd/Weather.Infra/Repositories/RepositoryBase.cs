using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Interfaces.Repositories;

namespace Weather.Infra.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }


        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }


        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);

            //_context.Entry(entity).Reload();
            _context.Entry(entity).State = EntityState.Modified;


            var propertyUpdatedAt = entity.GetType().GetProperty("UpdatedAt");
            propertyUpdatedAt.SetValue(entity, DateTime.UtcNow);

            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(object id)
        {
            TEntity? entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete != null)
                await DeleteAsync(entityToDelete);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            var propertyUpdatedAt = entity.GetType().GetProperty("UpdatedAt");
            propertyUpdatedAt.SetValue(entity, DateTime.Now);

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

    }
}
