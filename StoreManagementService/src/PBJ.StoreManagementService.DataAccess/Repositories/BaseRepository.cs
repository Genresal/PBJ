using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<List<TEntity>> GetAmountAsync(int amount)
        {
            return await _databaseContext.Set<TEntity>().AsNoTracking()
                .Take(amount).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _databaseContext.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual void Create(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Remove(entity);
        }
    }
}
