namespace PBJ.NotificationService.Domain.Dtos
{
    public class MailTemplateDto
    {
        public string TemplateKey { get; set; } = null!;

        public string TemplateFile { get; set; } = null!;

        public string Data { get; set; } = null!;
    }
}
