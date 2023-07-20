using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class UserFollowersRepository : BaseRepository<UserFollowers>, IUserFollowersRepository
    {
        public UserFollowersRepository(DatabaseContext databaseContext) : base(databaseContext)
        { }

        public async Task DeleteRangeAsync(IReadOnlyCollection<UserFollowers> collection)
        {
            _databaseContext.UserFollowers.RemoveRange(collection);

            await _databaseContext.SaveChangesAsync();
        }
    }
}
