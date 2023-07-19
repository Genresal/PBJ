using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAmountAsync(int amount);

        Task<TEntity> GetAsync(int id);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
