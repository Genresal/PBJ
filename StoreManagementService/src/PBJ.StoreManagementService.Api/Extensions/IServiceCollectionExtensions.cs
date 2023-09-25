using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Handlers;
using PBJ.StoreManagementService.Business.Options;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Enums;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static AuthOptions _authOptions = new();

        public static void BuildOptions(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection(AuthOptions.AuthConfigurations).Bind(_authOptions);
        }

        public static void SetupAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = _authOptions.Authority;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidTypes = new[] { "at + jwt" };
            });
        }

        public static void SetupAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {

                options.AddPolicy("User", policy =>
                {
                    policy.Requirements.Add(new UserRequirement(_authOptions.Authority, Role.User.ToString()));
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new UserRequirement(_authOptions.Authority, Role.Admin.ToString()));
                });
            });

            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();
        }

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

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SMS API",
                    Version = "v1"
                });

                options.AddSecurityDefinition(_authOptions.AuthScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = _authOptions.AuthScheme,
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = _authOptions.AuthScheme }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = _authOptions.AuthScheme
                            },
                            Scheme = "oauth2",
                            Name = _authOptions.AuthScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
