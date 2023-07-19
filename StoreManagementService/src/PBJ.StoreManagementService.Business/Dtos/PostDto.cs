namespace PBJ.StoreManagementService.Business.Dtos
{
    public class PostDto
    {
        public string? Content { get; set; }

        public DateTime PostDate { get; set; }

        public int UserId { get; set; }

        public UserDto? User { get; set; }

        public ICollection<CommentDto>? Comments { get; set; }
    }
}
