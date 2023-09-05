using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBJ.NotificationService.Domain.Options;

namespace PBJ.NotificationService.Domain.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDomainOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailBotOptions>(configuration.GetSection(MailBotOptions.MailBotConfigurations));
        }
    }
}
