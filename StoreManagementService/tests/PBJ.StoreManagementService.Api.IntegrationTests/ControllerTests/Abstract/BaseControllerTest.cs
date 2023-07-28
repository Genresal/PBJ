
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PBJ.StoreManagementService.Api.IntegrationTests.Configuration;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.Managers;
using PBJ.StoreManagementService.DataAccess.Context;
using System.Net.Http.Headers;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseControllerTest
    {
        protected TestWebApplicationFactory _factory;
        protected HttpClient _httpClient;
        protected TestDataManager _dataManager;
        protected Fixture _fixture;

        protected BaseControllerTest()
        {
            _factory = new TestWebApplicationFactory();
            _httpClient = _factory.CreateClient();
            _fixture = new Fixture();
            _dataManager = new TestDataManager(_factory.Services.CreateScope().ServiceProvider
                .GetRequiredService<DatabaseContext>(), _fixture);
        }

        protected async Task<(TReturn?, HttpResponseMessage)> GetAsync<TReturn>(
            string requestUri,
            bool isStatusCodeOnly = false)
        {
            var response = await _httpClient.GetAsync(requestUri);

            if (isStatusCodeOnly)
            {
                return (default, response);
            }

            var dto = JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());

            return (dto, response);
        }

        protected async Task<(TDto?, HttpResponseMessage)> PostAsync<TDto, TRequestModel>(string requestUri,
            TRequestModel requestModel,
            bool isStatusCodeOnly = false)
        {
            var requestBody = BuildRequestBody(requestModel);

            var response = await _httpClient.PostAsync(requestUri, requestBody);

            if (isStatusCodeOnly)
            {
                return (default, response);
            }

            var dto = JsonConvert.DeserializeObject<TDto>(await response.Content.ReadAsStringAsync());

            return (dto, response);
        }

        protected async Task<(TDto?, HttpResponseMessage)> PutAsync<TDto, TRequestModel>(string requestUri,
            TRequestModel requestModel,
            bool isStatusCodeOnly = false)
        {
            var requestBody = BuildRequestBody(requestModel);

            var response = await _httpClient.PutAsync(requestUri, requestBody);

            if (isStatusCodeOnly)
            {
                return (default, response);
            }

            var dto = JsonConvert.DeserializeObject<TDto>(await response.Content.ReadAsStringAsync());

            return (dto, response);
        }

        protected StringContent BuildRequestBody<TRequestModel>(TRequestModel requestModel)
        {
            var requestBody = new StringContent(JsonConvert.SerializeObject(requestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            return requestBody;
        }
    }
}
