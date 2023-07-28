using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static async Task MigrateDatabases(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var databaseContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

            await databaseContext!.Database.MigrateAsync();
        }
    }
}
