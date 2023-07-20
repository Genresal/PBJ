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

        public IReadOnlyCollection<Post>? Posts { get; set; }

        public IReadOnlyCollection<Comment>? Comments { get; set; }

        public IReadOnlyCollection<UserFollowers>? Followings { get; set; }

        public IReadOnlyCollection<UserFollowers>? Followers { get; set; }
    }
}
