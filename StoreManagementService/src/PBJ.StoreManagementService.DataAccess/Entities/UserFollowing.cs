using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class UserFollowing : BaseEntity
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int FollowingId { get; set; }

        public Following? Following { get; set; }
    }
}
