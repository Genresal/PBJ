using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class UserFollowingRepository : BaseRepository<UserFollowing>,
        IUserFollowingRepository
    {
        public UserFollowingRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }
    }
}
