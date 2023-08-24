using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    options.UserInteraction.LoginUrl = "/auth/sign-in";

                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
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
    }
}
