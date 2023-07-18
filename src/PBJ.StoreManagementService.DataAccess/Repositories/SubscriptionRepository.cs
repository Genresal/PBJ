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
                .Join(_databaseContext.UserSubscriptions,
                    s => s.Id,
                    us => us.SubscriptionId,
                    (s, us) => new
                    {
                        UserId = us.UserId,
                        Subscription = s
                    })
                .Where(x => x.UserId == userId).Take(amount)
                .Select(x => x.Subscription).ToListAsync();
        }
    }
}
