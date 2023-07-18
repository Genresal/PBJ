using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Post : BaseEntity
    {
        public string? Content { get; set; }

        public DateTime PostDate { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
