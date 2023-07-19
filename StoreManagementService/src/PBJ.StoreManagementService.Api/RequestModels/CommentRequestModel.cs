namespace PBJ.StoreManagementService.Api.RequestModels
{
    public class CommentRequestModel
    {
        public string? Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}
