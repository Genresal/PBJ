namespace PBJ.StoreManagementService.Models.UserFollowers
{
    public class UserFollowersRequestModel
    {
        public string UserEmail { get; set; } = null!;

        public string FollowerEmail { get; set; } = null!;
    }
}
