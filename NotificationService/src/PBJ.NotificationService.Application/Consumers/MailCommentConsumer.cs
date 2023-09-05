using MassTransit;
using PBJ.NotificationService.Domain.Abstract;
using PBJ.NotificationService.Domain.Constants;
using PBJ.NotificationService.Domain.Dtos;

namespace PBJ.NotificationService.Application.Consumers
{
    public class MailCommentConsumer : IConsumer<MailDto>
    {
        private readonly IMailService _mailService;

        public MailCommentConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<MailDto> context)
        {
            await _mailService.SendMessageAsync(context.Message.Email, new MailTemplateDto
            {
                TemplateFile = Templates.CommentTemplateFile,
                TemplateKey = Templates.CommentTemplate,
                Data = context.Message.Message
            });
        }
    }
}
