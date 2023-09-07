using MassTransit;
using PBJ.NotificationService.Core.Dtos;
using PBJ.NotificationService.Domain.Abstract;

namespace PBJ.NotificationService.Application.Consumers
{
    public class MailConsumer : IConsumer<MailDto>
    {
        private readonly IMailService _mailService;

        public MailConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<MailDto> context)
        {
            await _mailService.SendMessageAsync(context.Message.Email, context.Message.Message);
        }
    }
}
