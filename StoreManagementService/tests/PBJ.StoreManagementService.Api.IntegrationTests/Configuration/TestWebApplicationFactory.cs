using AutoFixture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Api.IntegrationTests.Managers;
using PBJ.StoreManagementService.Api.IntegrationTests.MockDependencies;
using PBJ.StoreManagementService.Business.Producers.Abstract;
using PBJ.StoreManagementService.DataAccess.Context;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Configuration
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                AddTestDatabase(services);

                services.AddScoped<TestDataManager>();
                services.AddScoped<Fixture>();

                SetupMockMassTransit(services);
            });
        }

        private static void SetupMockMassTransit(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(IMessageProducer));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddTransient<IMessageProducer, MockMessageProducer>();
        }

        private static void AddTestDatabase(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(DbContextOptions<DatabaseContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("pbjTest");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
