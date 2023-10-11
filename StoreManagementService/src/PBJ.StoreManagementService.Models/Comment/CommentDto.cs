using PBJ.StoreManagementService.Models.Post;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Models.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserEmail { get; set; }

        public UserDto User { get; set; }

        public int PostId { get; set; }

        public PostDto Post { get; set; }
    }
}
