using PBJ.NotificationService.Domain.Abstract;

namespace PBJ.NotificationService.Application.Services
{
    public class MailService : IMailService
    {
        public Task SendMessageAsync(string email, string message)
        {
            throw new NotImplementedException();
        }
    }
}
