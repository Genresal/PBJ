namespace PBJ.StoreManagementService.Business.Options
{
    public class ReactOidcOptions
    {
        public const string ReactOidcConfiguration = "ReactOidcConfiguration";

        public string Authority { get; set; } = null!;

        public string ClientId { get; set; } = null!;

        public string ClientSecret { get; set; } = null!;

        public string ResponseType { get; set; } = null!;
    }
}
