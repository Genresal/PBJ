namespace PBJ.StoreManagementService.Business.Dtos
{
    public class FollowingDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int FollowingUserId { get; set; }

        public UserDto? FollowingUser { get; set; }

        public IReadOnlyCollection<UserFollowingDto>? UserFollowings { get; set; }
    }
}
