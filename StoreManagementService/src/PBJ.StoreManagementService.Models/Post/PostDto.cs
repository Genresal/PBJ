using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Models.Post
{
    public class PostDto
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserEmail { get; set; } = null!;

        public UserDto? User { get; set; }

        public IReadOnlyCollection<CommentDto>? Comments { get; set; }
    }
}
