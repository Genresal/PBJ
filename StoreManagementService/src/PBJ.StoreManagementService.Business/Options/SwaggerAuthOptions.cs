namespace PBJ.StoreManagementService.Business.Options
{
    public class SwaggerAuthOptions
    {
        public const string SwaggerAuthConfiguration = "SwaggerAuthConfiguration";

        public string AuthorizationUrl { get; set; } = null!;

        public string TokenUrl { get; set; } = null!;

        public string Scope { get; set; } = null!;
    }
}
