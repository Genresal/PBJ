using AutoFixture;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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
        private const string TestAuthScheme = "TestScheme";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureTestServices(services =>
            {
                AddTestDatabase(services);

                services.AddScoped<TestDataManager>();
                services.AddScoped<Fixture>();

                SetupMockMassTransit(services);

                AddAuthentication(services);
            });
        }

        private static void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = TestAuthScheme;
                options.DefaultScheme = TestAuthScheme;
                options.DefaultChallengeScheme = TestAuthScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => {});
        }

        private static void SetupMockMassTransit(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(IMessageProducer));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddMassTransitTestHarness(options => 
            {
                options.AddDelayedMessageScheduler();

                options.UsingInMemory((context, config) => 
                {
                    config.UseDelayedMessageScheduler();

                    config.ConfigureEndpoints(context);
                });
            });

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
            }, ServiceLifetime.Scoped);
        }
    }
}
