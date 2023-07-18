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
                .Join(_databaseContext.UserFollowings,
                    f => f.Id,
                    uf => uf.FollowingId,
                    (f, uf) => new 
                    {
                        UserId = uf.UserId,
                        Following = f
                    })
                .Where(x => x.UserId == userId).Select(x => x.Following)
                .ToListAsync();
        }
    }
}
