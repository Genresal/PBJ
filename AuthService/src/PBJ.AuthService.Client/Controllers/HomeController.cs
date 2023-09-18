using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace PBJ.AuthService.Client.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            //get access token
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient
                .GetDiscoveryDocumentAsync("https://localhost:7069/");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "ApiOne",
                });

            //get secret data
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken!);

            var apiResponse = await apiClient.GetAsync("https://localhost:7018/api/secret");

            var content = await apiResponse.Content.ReadAsStringAsync();

            return Ok(new
            {
                AccessToken = tokenResponse.AccessToken,
                Message = content
            });
        }
    }
}
