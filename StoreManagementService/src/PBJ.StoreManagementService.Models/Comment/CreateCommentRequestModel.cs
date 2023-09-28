namespace PBJ.StoreManagementService.Models.Comment
{
    public class CreateCommentRequestModel
    {
        public string? Content { get; set; }

        public string UserEmail { get; set; } = null!;

        public int PostId { get; set; }
    }
}
