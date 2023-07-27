using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void MigrateDatabases(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var databaseContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

            databaseContext!.Database.Migrate();
        }
    }
}
