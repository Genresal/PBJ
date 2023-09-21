namespace PBJ.StoreManagementService.Business.Options
{
    public class AuthOptions
    {
        public const string AuthConfigurations = "AuthConfigurations";

        public string Authority { get; set; } = null!;

        public string AuthScheme { get; set; } = null!;
    }
}
