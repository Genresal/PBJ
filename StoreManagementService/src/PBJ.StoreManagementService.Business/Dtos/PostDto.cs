namespace PBJ.StoreManagementService.Business.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public UserDto? User { get; set; }

        public IReadOnlyCollection<CommentDto>? Comments { get; set; }
    }
}
