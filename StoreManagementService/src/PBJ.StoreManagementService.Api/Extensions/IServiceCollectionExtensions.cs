using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements;
using PBJ.StoreManagementService.Business.Options;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Enum;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static ReactOidcOptions _reactOidcOptions = new ReactOidcOptions();

        public static void BuildOptions(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection(ReactOidcOptions.ReactOidcConfiguration).Bind(_reactOidcOptions);
        }

        public static void SetupAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = _reactOidcOptions.Authority;
                options.ClientId = _reactOidcOptions.ClientId;
                options.ClientSecret = _reactOidcOptions.ClientSecret;
                options.ResponseType = _reactOidcOptions.ResponseType!;

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.ClaimActions.MapJsonKey(ClaimTypes.Email, ClaimTypes.Email);
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, ClaimTypes.Name);
                options.ClaimActions.MapJsonKey(ClaimTypes.Role, ClaimTypes.Role);
            });
        }

        public static void SetupAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.Requirements.Add(new UserRequirement(_reactOidcOptions.Authority, new List<string>
                    {
                        Role.User.ToString()
                    }));
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new UserRequirement(_reactOidcOptions.Authority, new List<string>
                    {
                        Role.User.ToString(),
                        Role.Admin.ToString()
                    }));
                });
            });
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
    }
}
