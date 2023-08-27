namespace PBJ.AuthService.Api.RequestModels
{
    public class LoginRequestModel
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ReturnUrl { get; set; } = null!;
    }
}
