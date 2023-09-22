namespace PBJ.StoreManagementService.Business.Producers.Abstract
{
    public interface IMessageProducer
    {
        Task PublicCommentMessageAsync(string email, string message);
    }
}
