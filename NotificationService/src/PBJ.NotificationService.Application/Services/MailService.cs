using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using PBJ.NotificationService.Domain.Abstract;
using PBJ.NotificationService.Domain.Dtos;
using PBJ.NotificationService.Domain.Options;

namespace PBJ.NotificationService.Application.Services
{
    public class MailService : IMailService
    {
        private readonly MailBotOptions _botOptions;
        private readonly IMailBodyGenerator _mailBodyGenerator;

        public MailService(IOptions<MailBotOptions> options, IMailBodyGenerator mailBodyGenerator)
        {
            _botOptions = options.Value;
            _mailBodyGenerator = mailBodyGenerator;
        }

        public async Task SendMessageAsync(string email, MailTemplateDto mailTemplateDto)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("PBJ", _botOptions.Login));
            mimeMessage.To.Add(new MailboxAddress("User", email));

            mimeMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = await _mailBodyGenerator.GenerateBodyAsync(mailTemplateDto)
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync(_botOptions.Login, _botOptions.Password);

                await client.SendAsync(mimeMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
