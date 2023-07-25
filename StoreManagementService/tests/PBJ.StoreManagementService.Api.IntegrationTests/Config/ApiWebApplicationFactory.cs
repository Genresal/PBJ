using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using PBJ.StoreManagementService.Api.IntegrationTests.Extensions;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Config
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("INTEGRATION_TESTS");

            builder.ConfigureTestServices(services =>
            {
                services.AddInMemoryDatabase();
            });
        }
    }
}
