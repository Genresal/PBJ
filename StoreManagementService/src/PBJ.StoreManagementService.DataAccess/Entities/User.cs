using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Email { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<UserFollowers>? Followings { get; set; }

        public ICollection<UserFollowers>? Followers { get; set; }
    }
}
