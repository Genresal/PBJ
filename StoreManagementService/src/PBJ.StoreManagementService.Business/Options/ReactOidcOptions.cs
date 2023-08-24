namespace PBJ.StoreManagementService.Business.Options
{
    public class ReactOidcOptions
    {
        public const string ReactOidcConfiguration = "ReactOidcConfiguration";

        public string? Authority { get; set; }

        public string? ClientId { get; set; }

        public string? ClientSecret { get; set; }

        public string? ResponseType { get; set; }
    }
}
