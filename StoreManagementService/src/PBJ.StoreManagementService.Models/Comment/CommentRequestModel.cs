namespace PBJ.StoreManagementService.Models.Comment
{
    public class CommentRequestModel
    {
        public string? Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}
