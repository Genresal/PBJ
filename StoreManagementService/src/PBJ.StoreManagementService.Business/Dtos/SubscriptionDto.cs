namespace PBJ.StoreManagementService.Business.Dtos
{
    public class SubscriptionDto
    {
        public DateTime SubscriptionDate { get; set; }

        public int SubscribedUserId { get; set; }

        public UserDto? SubscribedUser { get; set; }

        public ICollection<UserSubscriptionDto>? UserSubscriptions { get; set; }
    }
}
