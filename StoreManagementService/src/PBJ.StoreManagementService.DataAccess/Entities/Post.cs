using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Post : BaseEntity
    {
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserEmail { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}