using MassTransit;
using PBJ.Shared.QueueContext.MailDtos;
using PBJ.StoreManagementService.Business.Producers.Abstract;

namespace PBJ.StoreManagementService.Business.Producers
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublicCommentMessageAsync(string email, string message)
        {
            await _publishEndpoint.Publish(new MailDto(email, message));
        }
    }
}
