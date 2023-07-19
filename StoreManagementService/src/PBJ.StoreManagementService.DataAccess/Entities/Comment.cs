using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public string? Content { get; set; }

        public DateTime CommentDate { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int PostId { get; set; }

        public Post? Post { get; set; }
    }
}
