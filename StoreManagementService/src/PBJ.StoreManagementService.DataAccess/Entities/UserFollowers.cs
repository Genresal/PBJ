using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class UserFollowers : BaseEntity
    {
        public string UserEmail { get; set; } = null!;

        public User? User { get; set; }

        public string FollowerEmail { get; set; } = null!;

        public User? Follower { get; set; }
    }
}
