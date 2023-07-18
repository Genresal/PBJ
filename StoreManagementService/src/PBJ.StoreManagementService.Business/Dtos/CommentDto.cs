namespace PBJ.StoreManagementService.Business.Dtos
{
    public class CommentDto
    {
        public string? Content { get; set; }

        public DateTime CommentDate { get; set; }

        public int UserId { get; set; }

        public UserDto? User { get; set; }

        public int PostId { get; set; }

        public PostDto? Post { get; set; }
    }
}
