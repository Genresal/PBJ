using MassTransit;
using PBJ.NotificationService.Domain.Abstract;
using PBJ.NotificationService.Domain.Constants;
using PBJ.NotificationService.Domain.Dtos;
using PBJ.Shared.QueueContext.Comment;

namespace PBJ.NotificationService.Application.Consumers
{
    public class MailConsumer : IConsumer<CommentMail>
    {
        private readonly IMailService _mailService;

        public MailConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<CommentMail> context)
        {
            await _mailService.SendMessageAsync(context.Message.email, new MailTemplateDto()
            {
                TemplateFile = Templates.CommentTemplateFile,
                TemplateKey = Templates.CommentTemplate,
                Data = context.Message.message
            });
        }
    }
}
