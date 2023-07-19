using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<List<Subscription>> GetUserSubscriptionsAsync(int userId, int amount);
    }
}
