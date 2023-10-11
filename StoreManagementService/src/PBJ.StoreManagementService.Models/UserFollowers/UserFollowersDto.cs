using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Models.UserFollowers
{
    public class UserFollowersDto
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public UserDto User { get; set; }

        public string FollowerEmail { get; set; }

        public UserDto Follower { get; set; }
    }
}
