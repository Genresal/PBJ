using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Post;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Models.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public bool IsFollowingRequestUser { get; set; } = true;

        public IReadOnlyCollection<PostDto>? Posts { get; set; }

        public IReadOnlyCollection<CommentDto>? Comments { get; set; }

        public IReadOnlyCollection<UserFollowersDto>? Followings { get; set; }

        public IReadOnlyCollection<UserFollowersDto>? Followers { get; set; }
    }
}
