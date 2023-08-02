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
            services.AddIdentity<AuthUser, AuthRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void SetupIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddIdentityServer(options =>
                {
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
