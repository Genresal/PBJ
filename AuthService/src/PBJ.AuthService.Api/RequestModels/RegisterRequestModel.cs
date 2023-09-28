namespace PBJ.AuthService.Api.RequestModels
{
    public class RegisterRequestModel
    {
        public string UserName { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public string ReturnUrl { get; set; } = null!;
    }
}
