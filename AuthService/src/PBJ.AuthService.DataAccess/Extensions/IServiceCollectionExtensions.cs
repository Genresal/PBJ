using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBJ.AuthService.DataAccess.Context;

namespace PBJ.AuthService.DataAccess.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAuthDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.EnableRetryOnFailure();
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });
        }

        public static void MigrateDatabase(this IServiceCollection services)
        {
            services.BuildServiceProvider().CreateScope().ServiceProvider
                .GetRequiredService<AuthDbContext>().Database.Migrate();
        }
    }
}
