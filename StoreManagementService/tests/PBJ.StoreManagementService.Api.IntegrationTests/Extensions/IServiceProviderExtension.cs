using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Extensions
{
    public static class IServiceProviderExtension
    {
        public static async Task SeedDataAsync<TEntity>(this IServiceProvider provider, TEntity entity)
        where TEntity : BaseEntity
        {
            var databaseContext = provider.GetService<DatabaseContext>();

            databaseContext.Set<TEntity>().Add(entity);

            await databaseContext.SaveChangesAsync();
        }
    }
}
