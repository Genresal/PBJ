using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class UserSubscriptionRepository : BaseRepository<UserSubscription>,
        IUserSubscriptionRepository
    {
        public UserSubscriptionRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }
    }
}
