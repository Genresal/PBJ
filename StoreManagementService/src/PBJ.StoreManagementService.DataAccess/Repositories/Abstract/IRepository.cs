using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using System.Linq.Expressions;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAmountAsync(int amount);

        Task<TEntity> GetAsync(int id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
