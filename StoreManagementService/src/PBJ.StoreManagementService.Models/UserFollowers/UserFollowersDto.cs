using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Models.UserFollowers
{
    public class UserFollowersDto
    {
        public int Id { get; set; }

        public string UserEmail { get; set; } = null!;

        public UserDto? User { get; set; }

        public string FollowerEmail { get; set; } = null!;

        public UserDto? Follower { get; set; }
    }
}
