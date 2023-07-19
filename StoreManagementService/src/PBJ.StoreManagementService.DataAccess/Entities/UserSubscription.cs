using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class UserSubscription : BaseEntity
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int SubscriptionId { get; set; }

        public Subscription? Subscription { get; set; }
    }
}
