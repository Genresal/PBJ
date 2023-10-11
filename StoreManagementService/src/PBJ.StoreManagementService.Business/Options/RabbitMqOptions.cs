namespace PBJ.StoreManagementService.Business.Options
{
    public class RabbitMqOptions
    {
        public const string RabbitMqConfigurations = "RabbitMqConfigurations";

        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
