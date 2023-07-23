using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using PBJ.StoreManagementService.DataAccess.Repositories;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Extensions
{
    public static class IServiceProviderExtension
    {
        public static async Task SeedDataAsync<TRepository, TEntity>(this IServiceProvider provider, TEntity entity)
        where TEntity : BaseEntity
        where TRepository : BaseRepository<TEntity>
        {
            var repository = provider.GetService<TRepository>();

            await repository.CreateAsync(entity);
            await repository.CreateAsync(entity);
        }
    }
}
