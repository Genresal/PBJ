using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<List<Subscription>> GetUserSubscriptionsAsync(int userId, int amount)
        {
            return await _databaseContext.Subscriptions.AsNoTracking()
                .Include(x => x.UserSubscriptions.Where(us => us.UserId == userId)).ToListAsync();
        }
    }
}
