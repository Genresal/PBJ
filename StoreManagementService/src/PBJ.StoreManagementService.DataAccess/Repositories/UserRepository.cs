using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<PaginationResponse<User>> GetFollowersAsync(string userEmail, int page, int take)
        {
            var (items, count) = await ExecuteQueryWithIncludeAsync(page, take,
                where: x => x.Followings!.Any(uf => uf.UserEmail == userEmail),
                orderBy: x => x.Id,
                include: x => x.Followers);

            return new PaginationResponse<User>
            {
                Page = page,
                PageSize = take,
                Items = items,
                Total = count
            };
        }

        protected async Task<(List<User>, int)> ExecuteQueryWithIncludeAsync<TOrder, TInclude>(int page, int take, 
            Expression<Func<User, bool>> where = null, 
            Expression<Func<User, TOrder>> orderBy = null,
            Expression<Func<User, TInclude>> include = null,
            bool ascOrder = true)
        {

            var query = _databaseContext.Set<User>().AsQueryable();

            if (include != null)
            {
                query = query.Include(include);
            }

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
