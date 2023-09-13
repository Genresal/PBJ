namespace PBJ.StoreManagementService.Business.Options
{
    public class RabbitMqOptions
    {
        public const string RabbitMqConfigurations = "RabbitMqConfigurations";

        public string Host { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
