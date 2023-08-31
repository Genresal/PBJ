using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PBJ.NotificationService.Application.Consumers;

namespace PBJ.NotificationService.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void SetupMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(configurations =>
            {
                configurations.AddConsumer<MailConsumer>();
                configurations.UsingRabbitMq();
            });
        }
    }
}
