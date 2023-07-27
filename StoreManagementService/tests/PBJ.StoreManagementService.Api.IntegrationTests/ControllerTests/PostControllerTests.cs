using AutoFixture;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Models.Post;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class PostControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            //Act
            var (postDtos, response) = await GetAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/{amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.AllBeOfType<PostDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/error", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(2)]
        public async Task GetUserPostsAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var (postDtos, response) = await GetAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/user?userId={post.UserId}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.AllBeOfType<PostDto>();
            postDtos?.ForEach(x => x.UserId.Should().Be(post.UserId));
        }

        [Theory]
        [InlineData(2)]
        public async Task GetUserPostsAsync_WhenUserIdIsZero_ReturnsOK(int amount)
        {
            //Arrange
            //Act
            var (postDtos, response) = await GetAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/user?userId={0}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.HaveCount(0);
        }

        [Theory]
        [InlineData(2)]
        public async Task GetUserPostsAsync_WhenUserIdIsNotValid_ReturnsBadRequest(int amount)
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/user?userId={string.Empty}&amount={amount}", 
                isStatusCodeOnly:true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetUserPostsAsync_WhenAmountIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/user?userId={1}&amount={string.Empty}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var (postDto, response) = await GetAsync<PostDto>($"{TestingConstants.PostApi}?id={post.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<PostDto>(
                $"{TestingConstants.PostApi}?id={-1}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<PostDto>(
                $"{TestingConstants.PostApi}?id={string.Empty}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var postRequestModel = _fixture.Build<PostRequestModel>()
                .With(x => x.UserId, user.Id).Create();

            //Act
            var (postDto, response) = await PostAsync<PostDto, PostRequestModel>(
                TestingConstants.PostApi, postRequestModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto?.UserId.Should().Be(postRequestModel.UserId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var postRequestModel = _fixture.Build<PostRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            //Act
            var (_, response) = await PostAsync<PostDto, PostRequestModel>(
                TestingConstants.PostApi, postRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            var postRequestModel = _fixture.Build<PostRequestModel>()
                .With(x => x.UserId, post.UserId).Create();

            //Act
            var (postDto, response) = await PutAsync<PostDto, PostRequestModel>(
                $"{TestingConstants.PostApi}?id={post.Id}", postRequestModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto?.UserId.Should().Be(postRequestModel.UserId);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var postRequestModel = _fixture.Create<PostRequestModel>();

            //Act
            var (_, response) = await PutAsync<PostDto, PostRequestModel>(
                $"{TestingConstants.PostApi}?id={0}", postRequestModel, isStatusCodeOnly: true);

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

            //Act
            var (_, response) = await PutAsync<PostDto, PostRequestModel>(
                $"{TestingConstants.PostApi}?id={id}", postRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
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

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.PostApi}?id={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
