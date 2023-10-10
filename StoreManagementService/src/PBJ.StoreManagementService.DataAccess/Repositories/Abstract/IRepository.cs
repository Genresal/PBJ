using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using System.Linq.Expressions;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<PaginationResponse<TEntity>> GetPaginatedAsync<TProperty>(int page, int take,
            Expression<Func<TEntity, bool>>? where = null,
            Expression<Func<TEntity, TProperty>>? orderBy = null,
            bool acsOrder = true);

        Task<TEntity?> GetAsync(int id);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity? entity);
    }
}
