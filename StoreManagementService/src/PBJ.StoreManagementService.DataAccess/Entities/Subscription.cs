using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Subscription : BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public int SubscribedUserId { get; set; }

        public User? SubscribedUser { get; set; }

        public IReadOnlyCollection<UserSubscription>? UserSubscriptions { get; set; }
    }
}
