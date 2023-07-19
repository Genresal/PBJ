using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class UserFollowers : BaseEntity
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int FollowerId { get; set; }

        public User? Follower { get; set; }
    }
}
