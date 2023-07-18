using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Following : BaseEntity
    {
        public DateTime FollowingDate { get; set; }

        public int FollowingUserId { get; set; }

        public User? FollowingUser { get; set; }

        public ICollection<UserFollowing>? UserFollowings { get; set; }
    }
}
