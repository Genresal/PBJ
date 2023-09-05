using PBJ.NotificationService.Domain.Dtos;

namespace PBJ.NotificationService.Domain.Abstract
{
    public interface IMailBodyGenerator
    {
        Task<string> GenerateBodyAsync(MailTemplateDto mailTemplate);
    }
}
