namespace PBJ.AuthService.Api.RequestModels
{
    public class EditPasswordRequestModel
    {
        public string CurrentPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
