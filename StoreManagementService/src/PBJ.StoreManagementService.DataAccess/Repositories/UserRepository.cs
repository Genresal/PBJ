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

        public async Task<List<User>> GetFollowersAsync(int userId, int amount)
        {

                return await _databaseContext.Users
                    .Where(x => x.Followings.Any(uf => uf.UserId == userId))
                    .Take(amount)
                    .ToListAsync();
        }

        public async Task<List<User>> GetFollowingsAsync(int followerId, int amount)
        {
            return await _databaseContext.Users
                .Where(x => x.Followers.Any(uf => uf.FollowerId == followerId))
                .Take(amount)
                .ToListAsync();
        }
    }
}
