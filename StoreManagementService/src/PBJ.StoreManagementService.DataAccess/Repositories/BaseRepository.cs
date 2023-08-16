using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<PaginationResponse<TEntity>> GetPaginatedAsync<TProperty>(
            int page, int take,
            Expression<Func<TEntity, bool>>? where = null,
            Expression<Func<TEntity, TProperty>>? orderBy = null,
            bool ascOrder = true)
        {
            var (items, count) = await ExecuteQueryAsync(page, take, where, orderBy, ascOrder);

            return new PaginationResponse<TEntity>
            {
                Page = page,
                PageSize = take,
                Items = items,
                Total = count
            };
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return (await _databaseContext.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return (await _databaseContext.Set<TEntity>()
                .FirstOrDefaultAsync(whereExpression))!;
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

        public virtual async Task DeleteAsync(TEntity? entity)
        {
            _databaseContext.Set<TEntity>().Remove(entity!);

            await _databaseContext.SaveChangesAsync();
        }

        protected async Task<(List<TEntity>, int)> ExecuteQueryAsync<TProperty>(
            int page, int take,
            Expression<Func<TEntity, bool>>? where = null,
            Expression<Func<TEntity, TProperty>>? orderBy = null,
            bool ascOrder = true)
        {
            var query = _databaseContext.Set<TEntity>().AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = ascOrder ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            var items = await query.Skip((page - 1) * take).Take(take).ToListAsync();

            var count = await query.CountAsync();

            return (items, count);
        }
    }
}
