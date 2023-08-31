namespace PBJ.NotificationService.Domain.Abstract
{
    public interface IMessageBodyGenerator
    {
        Task GenerateMessageAsync(string message);
    }
}
