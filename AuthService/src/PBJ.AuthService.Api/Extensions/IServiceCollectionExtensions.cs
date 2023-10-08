using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PBJ.AuthService.Api.RequestModels;
using PBJ.AuthService.Api.Validators;
using PBJ.AuthService.Business.Services;
using PBJ.AuthService.DataAccess.Context;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void SetupIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AuthUser, AuthRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void SetupIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/api/auth/login";

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddAspNetIdentity<AuthUser>()
            .AddProfileService<ProfileService>()
            .AddConfigurationStore<AuthDbContext>(options =>
            {
                options.ConfigureDbContext = builder =>
                {
                    builder.UseSqlServer(connectionString);
                    builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                };
            })
            .AddOperationalStore<AuthDbContext>(options =>
            {
                options.ConfigureDbContext = builder =>
                {
                    builder.UseSqlServer(connectionString);
                    builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 3600;
                };
            })
            .AddDeveloperSigningCredential();
        }

        public static void SetupIdentityServerCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/api/auth/login";
                options.LogoutPath = "/api/auth/logout";
                options.Cookie.Name = "IdentityServer.Cookies";
            });
        }

        public static void AddValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
        }

        public static void AddJwtBearerAuthentication(this IServiceCollection services)
        {
            services.AddLocalApiAuthentication();
        }
    }
}
