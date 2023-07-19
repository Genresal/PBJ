namespace PBJ.StoreManagementService.Business.Dtos
{
    public class SubscriptionDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int SubscribedUserId { get; set; }

        public UserDto? SubscribedUser { get; set; }

        public IReadOnlyCollection<UserSubscriptionDto>? UserSubscriptions { get; set; }
    }
}
