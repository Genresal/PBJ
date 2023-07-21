using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.DataAccess.Context;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddInMemoryDatabase(this IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(DbContextOptions<DatabaseContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("sms");
            });
        }
    }
}
