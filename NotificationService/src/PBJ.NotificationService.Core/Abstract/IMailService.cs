namespace PBJ.NotificationService.Domain.Abstract
{
    public interface IMailService
    {
        Task SendMessageAsync(string email, string message);
    }
}
