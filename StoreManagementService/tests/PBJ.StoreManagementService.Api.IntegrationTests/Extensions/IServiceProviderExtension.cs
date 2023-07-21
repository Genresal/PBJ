using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.DataAccess.Entities.Abstract;
using PBJ.StoreManagementService.DataAccess.Repositories;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Extensions
{
    public static class IServiceProviderExtension
    {
        private static readonly Fixture _fixture;

        static IServiceProviderExtension()
        {
            _fixture = new Fixture();
        }

        public static async Task SeedDataAsync<TRepository, TEntity>(this IServiceProvider provider)
        where TEntity : BaseEntity
        where TRepository : BaseRepository<TEntity>
        {
            var repository = provider.GetService<TRepository>();

            await repository.CreateAsync(_fixture.Create<TEntity>());
            await repository.CreateAsync(_fixture.Create<TEntity>());
        }
    }
}
