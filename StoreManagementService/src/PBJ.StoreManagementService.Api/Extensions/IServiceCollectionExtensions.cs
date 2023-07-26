using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddNewtonsoftJson(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
        }

        public static void BuildSerilog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .WriteTo.Async(options =>
                {
                    options.Console(restrictedToMinimumLevel: LogEventLevel.Information);
                    options.File("logs/log-.txt", rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: LogEventLevel.Error);
                })
                .CreateLogger();
        }
    }
}
