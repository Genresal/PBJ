namespace PBJ.NotificationService.Domain.Options
{
    public class SmtpOptions
    {
        public const string SmtpConfigurations = "SmtpConfigurations";

        public string SmtpHost { get; set; } = null!;

        public int SmtpPort { get; set; }

        public bool UseSsl { get; set; } = true;
    }
}
