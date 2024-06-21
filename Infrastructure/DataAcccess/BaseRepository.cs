using Entity;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAcccess
{
    public class BaseRepository<TEntity,TEntityId, TContext> : IBaseRepository<TEntity,TEntityId> where TEntity : BaseEntity<TEntityId>, new() 
                                                                           where TContext : DbContext, new()
                                                                            
    {
        public async Task DeleteAsync(TEntity entity)
        {
            using var context = new TContext();
            var delete = context.Entry(entity);
            delete.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using var context = new TContext();
            IQueryable<TEntity> dbSet =  context.Set<TEntity>();
            if (expression==null)
            {
                return await dbSet.ToListAsync();
            }
            return await  dbSet.Where(expression).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            using var context = new TContext();
            IQueryable<TEntity> dbSet = context.Set<TEntity>();
            var a =  await dbSet.SingleOrDefaultAsync(expression);
            return a;
        }

        public async Task<TEntity> GetByIdAsync(TEntityId entityId)
        {
            using var context = new TContext();
            IQueryable<TEntity> dbSet = context.Set<TEntity>();
            return await dbSet.SingleOrDefaultAsync(p => p.Id.Equals(entityId) && p.IsActive==true);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using var context = new TContext();
            var insert = context.Entry(entity);
            insert.State = EntityState.Added;
            await context.SaveChangesAsync();
            return insert.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var context = new TContext();
            var update = context.Entry(entity);
            update.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return update.Entity;
        }
    }
}
