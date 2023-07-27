using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

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
                .WriteTo.Logger(configuration =>
                {
                    configuration.Filter.ByIncludingOnly(e =>
                        e.Level == LogEventLevel.Information);
                    configuration.WriteTo.Console();
                })
                .WriteTo.Logger(configuration =>
                {
                    configuration.Filter.ByIncludingOnly(e =>
                        e.Level == LogEventLevel.Information);
                    configuration.WriteTo.File("bin/Debug/logs/log-information-.txt",
                        rollingInterval: RollingInterval.Month);
                })
                .WriteTo.Logger(configuration =>
                {
                    configuration.Filter.ByIncludingOnly(e =>
                        e.Level == LogEventLevel.Error);
                    configuration.WriteTo.File("bin/Debug/logs/log-errors-.txt",
                        rollingInterval: RollingInterval.Month);
                })
                .CreateLogger();
        }
    }
}
