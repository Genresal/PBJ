using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Post;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Models.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool IsFollowingRequestUser { get; set; } = false;

        public ICollection<PostDto> Posts { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

        public ICollection<UserFollowersDto> Followings { get; set; }

        public ICollection<UserFollowersDto> Followers { get; set; }
    }
}
