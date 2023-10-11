namespace PBJ.StoreManagementService.Models.Comment
{
    public class CreateCommentRequestModel
    {
        public string Content { get; set; }

        public string UserEmail { get; set; }

        public int PostId { get; set; }
    }
}
