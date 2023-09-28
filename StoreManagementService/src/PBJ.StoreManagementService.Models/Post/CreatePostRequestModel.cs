namespace PBJ.StoreManagementService.Models.Post
{
    public class CreatePostRequestModel
    {
        public string? Content { get; set; }

        public string UserEmail { get; set; } = null!;
    }
}
