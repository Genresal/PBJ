using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Business.Options;
using PBJ.StoreManagementService.Business.Producers;
using PBJ.StoreManagementService.Business.Producers.Abstract;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.Services.Abstract;
using System.Reflection;

namespace PBJ.StoreManagementService.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static RabbitMqOptions _rabbitMqOptions;

        public static void SetupOptions(this IServiceCollection services, IConfiguration configuration)
        {
            _rabbitMqOptions = new RabbitMqOptions();
            configuration.GetSection(RabbitMqOptions.RabbitMqConfigurations).Bind(_rabbitMqOptions);
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserFollowersService, UserFollowersService>();
        }

        public static void SetupMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.UsingRabbitMq((context, rbConfig) =>
                {
                    rbConfig.Host(_rabbitMqOptions!.Host, hostConfigurator =>
                    {
                        hostConfigurator.Username(_rabbitMqOptions.Username);
                        hostConfigurator.Password(_rabbitMqOptions.Password);
                    });

                    rbConfig.ConfigureEndpoints(context);
                });
            });

            services.AddTransient<IMessageProducer, MessageProducer>();
        }
    }
}
