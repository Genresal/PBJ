using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Api.IntegrationTests.Configuration;
using PBJ.StoreManagementService.Api.IntegrationTests.Managers;
using PBJ.StoreManagementService.DataAccess.Context;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract
{
    public abstract class BaseControllerTest
    {
        protected TestWebApplicationFactory _factory;
        protected HttpClient _httpClient;
        protected TestDataManager _dataManager;
        protected Fixture _fixture;

        public BaseControllerTest()
        {
            _factory = new TestWebApplicationFactory();
            _httpClient = _factory.CreateClient();
            _fixture = new Fixture();
            _dataManager = new TestDataManager(_factory.Services.CreateScope().ServiceProvider
                .GetRequiredService<DatabaseContext>(), _fixture);
        }
    }
}
