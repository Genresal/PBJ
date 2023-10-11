namespace PBJ.AuthService.Api.RequestModels
{
    public class EditUserRequestModel
    {
        public string UserName { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public DateTime BirthDate { get; set; }
    }
}
