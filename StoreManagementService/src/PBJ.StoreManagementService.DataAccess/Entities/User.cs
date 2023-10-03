using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;

        public ICollection<Post>? Posts { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<UserFollowers>? Followings { get; set; }

        public ICollection<UserFollowers>? Followers { get; set; }
    }
}
