using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class UserFollowers : BaseEntity
    {
        public string UserEmail { get; set; }

        public User User { get; set; }

        public string FollowerEmail { get; set; }

        public User Follower { get; set; }
    }
}
