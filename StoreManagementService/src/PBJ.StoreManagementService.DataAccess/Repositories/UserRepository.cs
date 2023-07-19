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
            return await _databaseContext.Users.Include(x => x.Followers)
                .Where(x => x.Id == userId).Take(amount).ToListAsync();
        }

        public async Task<List<User>> GetFollowingsAsync(int userId, int amount)
        {
            return await _databaseContext.Users.Include(x => x.Followings)
                .Where(x => x.Id == userId).Take(amount).ToListAsync();
        }
    }
}
