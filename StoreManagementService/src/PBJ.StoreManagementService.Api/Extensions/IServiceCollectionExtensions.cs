using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Enums;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Handlers;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements;
using PBJ.StoreManagementService.Business.Options;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Security.Claims;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static ReactOidcOptions _reactOidcOptions = new();
        private static SwaggerAuthOptions _swaggerAuthOptions = new();

        public static void BuildOptions(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.GetSection(ReactOidcOptions.ReactOidcConfiguration).Bind(_reactOidcOptions);
            configuration.GetSection(SwaggerAuthOptions.SwaggerAuthConfiguration).Bind(_swaggerAuthOptions);
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
                    policy.Requirements.Add(new UserRequirement(_reactOidcOptions.Authority, Role.User.ToString()));
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new UserRequirement(_reactOidcOptions.Authority, Role.Admin.ToString()));
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

                options.AddSecurityDefinition("aouth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(_swaggerAuthOptions.AuthorizationUrl),
                            TokenUrl = new Uri(_swaggerAuthOptions.TokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { _swaggerAuthOptions.Scope, "Full access to sms api" }
                            }
                        }
                    }
                });
            });
        }
    }
}
