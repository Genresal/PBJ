namespace PBJ.NotificationService.Domain.Options
{
    public class MailBotOptions
    {
        public const string MailBotConfigurations = "MailBotConfigurations";

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
