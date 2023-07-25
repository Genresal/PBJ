using AutoFixture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Api.IntegrationTests.Managers;
using PBJ.StoreManagementService.DataAccess.Context;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Configuration
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices((context, services) =>
            {

                AddTestDatabase(services, context.Configuration);


                services.AddScoped<TestDataManager>();
                services.AddScoped<Fixture>();
            });
        }

        private IServiceCollection AddTestDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(DbContextOptions<DatabaseContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }
    }
}
