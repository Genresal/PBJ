namespace PBJ.StoreManagementService.Models.Comment
{
    public class CreateCommentRequestModel
    {
        public string? Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}
