using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.Post;
using System.Net;
using System.Net.Http.Headers;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class PostControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.PostApi}/{amount}");

            var postDtos = JsonConvert.DeserializeObject<List<PostDto>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.AllBeOfType<PostDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.PostApi}/error");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(2)]
        public async Task GetUserPostsAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.PostApi}/user?userId={post.UserId}&amount={amount}");

            var postDtos = JsonConvert.DeserializeObject<List<PostDto>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.AllBeOfType<PostDto>();
            postDtos.ForEach(x => x.UserId.Should().Be(post.UserId));
        }

        [Theory]
        [InlineData(2)]
        public async Task GetUserPostsAsync_WhenUserIdIsZero_ReturnsEmptyListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.PostApi}/user?userId={0}&amount={amount}");

            var postDtos = JsonConvert.DeserializeObject<List<Post>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.HaveCount(0);
        }

        [Theory]
        [InlineData(2)]
        public async Task GetUserPostsAsync_WhenUserIdIsNotValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.PostApi}/user?userId={string.Empty}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetUserPostsAsync_WhenAmountIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.PostApi}/user?userId={1}&amount={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsDto()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.PostApi}?id={post.Id}");

            var postDto = JsonConvert.DeserializeObject<PostDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.PostApi}?id={-1}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.PostApi}?id={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsCreatedDto()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var postRequestModel = _fixture.Build<PostRequestModel>()
                .With(x => x.UserId, user.Id).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(postRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.PostApi, requestBody);

            var postDto = JsonConvert.DeserializeObject<PostDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.UserId.Should().Be(postRequestModel.UserId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var postRequestModel = _fixture.Build<Post>()
                .With(x => x.Content, string.Empty).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(postRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.PostApi, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            var postRequestModel = _fixture.Build<PostRequestModel>()
                .With(x => x.UserId, post.UserId).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(postRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.PostApi}?id={post.Id}", requestBody);

            var postDto = JsonConvert.DeserializeObject<PostDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.UserId.Should().Be(postRequestModel.UserId);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var postRequestModel = _fixture.Create<PostRequestModel>();

            var requestBody = new StringContent(JsonConvert.SerializeObject(postRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.PostApi}?id={0}", requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var postRequestModel = _fixture.Build<PostRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(postRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.PostApi}?id={id}", requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.PostApi}?id={post.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.PostApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
