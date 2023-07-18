using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAmountAsync(int amount);

        Task<TEntity> GetAsync(int id);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
