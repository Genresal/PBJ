using AutoFixture;
using Azure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PBJ.StoreManagementService.Api.IntegrationTests.Configuration;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.Managers;
using PBJ.StoreManagementService.DataAccess.Context;
using System.Net.Http.Headers;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract
{
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

        protected async Task<HttpResponseMessage> ExecuteWithStatusCodeAsync(string requestUri,
            HttpMethod httpMethod, StringContent? requestBody = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUri, UriKind.Relative),
                Method = httpMethod,
                Content = requestBody
            };

            var response = await _httpClient.SendAsync(requestMessage);

            return response;
        }

        protected async Task<(TReturn?, HttpResponseMessage)> ExecuteWithFullResponseAsync<TReturn>(string requestUri,
            HttpMethod httpMethod, StringContent? requestBody = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUri, UriKind.Relative),
                Method = httpMethod,
                Content = requestBody
            };

            var response = await _httpClient.SendAsync(requestMessage);

            var dtoResult = JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());

            return (dtoResult, response);
        }

        protected static StringContent BuildRequestBody<TRequestModel>(TRequestModel requestModel)
        {
            var requestBody = new StringContent(JsonConvert.SerializeObject(requestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            return requestBody;
        }
    }
}
