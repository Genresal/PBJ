namespace PBJ.StoreManagementService.Business.Dtos
{
    public class UserFollowersDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserDto? User { get; set; }

        public int FollowerId { get; set; }

        public UserDto? Follower { get; set; }
    }
}
