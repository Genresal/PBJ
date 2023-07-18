using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Subscription : BaseEntity
    {
        public DateTime SubscriptionDate { get; set; }

        public int SubscribedUserId { get; set; }

        public User? SubscribedUser { get; set; }

        public ICollection<UserSubscription>? UserSubscriptions { get; set; }
    }
}
