namespace PBJ.StoreManagementService.Business.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Lastname { get; set; }

        public DateTime Birthdate { get; set; }

        public string? Login { get; set; }

        public IReadOnlyCollection<PostDto>? Posts { get; set; }

        public IReadOnlyCollection<CommentDto>? Comments { get; set; }

        public SubscriptionDto? Subscription { get; set; }

        public FollowingDto? Following { get; set; }

        public IReadOnlyCollection<UserSubscriptionDto>? UserSubscriptions { get; set; }

        public IReadOnlyCollection<UserFollowingDto>? UserFollowings { get; set; }
    }
}
