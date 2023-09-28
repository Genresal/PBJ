using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserEmail { get; set; } = null!;

        public User? User { get; set; }

        public int PostId { get; set; }

        public Post? Post { get; set; }
    }
}
