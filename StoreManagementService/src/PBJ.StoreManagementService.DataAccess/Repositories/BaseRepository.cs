using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using System.Linq.Expressions;

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

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _databaseContext.Set<TEntity>()
                .FirstOrDefaultAsync(where);
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Add(entity);

            await _databaseContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Update(entity);

            await _databaseContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Remove(entity);

            await _databaseContext.SaveChangesAsync();
        }
    }
}
