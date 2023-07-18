using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Business.Dtos
{
    public class UserDto
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Lastname { get; set; }

        public DateTime Birthdate { get; set; }

        public string? Login { get; set; }

        public ICollection<PostDto>? Posts { get; set; }

        public ICollection<CommentDto>? Comments { get; set; }

        public SubscriptionDto? Subscription { get; set; }

        public FollowingDto? Following { get; set; }

        public ICollection<UserSubscriptionDto>? UserSubscriptions { get; set; }

        public ICollection<UserFollowingDto>? UserFollowings { get; set; }
    }
}
