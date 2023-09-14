using MassTransit;
using PBJ.NotificationService.Domain.Abstract;
using PBJ.NotificationService.Domain.Constants;
using PBJ.NotificationService.Domain.Dtos;
using PBJ.Shared.QueueContext.MailDtos;

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
            await _mailService.SendMessageAsync(context.Message.email, new MailTemplateDto()
            {
                TemplateFile = Templates.MailTemplateFile,
                TemplateKey = Templates.MailTemplate,
                Data = context.Message.message
            });
        }
    }
}
