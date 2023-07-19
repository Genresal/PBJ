using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class FollowingRepository : BaseRepository<Following>, IFollowingRepository
    {
        public FollowingRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<List<Following>> GetUserFollowingsAsync(int userId, int amount)
        {
            return await _databaseContext.Followings.AsNoTracking()
                .Include(x => x.UserFollowings.Where(uf => uf.UserId == userId)).ToListAsync();
        }
    }
}
