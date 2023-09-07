using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PBJ.NotificationService.Application.Consumers;
using PBJ.NotificationService.Application.Generators;
using PBJ.NotificationService.Domain.Abstract;
using IMailService = PBJ.NotificationService.Domain.Abstract.IMailService;
using MailService = PBJ.NotificationService.Application.Services.MailService;

namespace PBJ.NotificationService.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void SetupMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(busConfig =>
            {
                busConfig.AddConsumer<MailCommentConsumer>();

                busConfig.UsingRabbitMq((context, rbConfig) =>
                {
                    rbConfig.Host("localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    rbConfig.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IMailBodyGenerator, MailBodyGenerator>();
        }
    }
}
