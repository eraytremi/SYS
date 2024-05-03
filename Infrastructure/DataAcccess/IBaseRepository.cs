using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAcccess
{
    public interface IBaseRepository<TEntity,TEntityId> where TEntity : class,new()
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> expression=null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TEntityId entityId);

    }
}
