using PBJ.NotificationService.Domain.Dtos;

namespace PBJ.NotificationService.Domain.Abstract
{
    public interface IMailService
    {
        Task SendMessageAsync(string email, MailTemplateDto mailTemplateDto);
    }
}
