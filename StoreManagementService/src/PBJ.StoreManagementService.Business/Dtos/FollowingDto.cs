namespace PBJ.StoreManagementService.Business.Dtos
{
    public class FollowingDto
    {
        public DateTime FollowingDate { get; set; }

        public int FollowingUserId { get; set; }

        public UserDto? FollowingUser { get; set; }

        public ICollection<UserFollowingDto>? UserFollowings { get; set; }
    }
}
