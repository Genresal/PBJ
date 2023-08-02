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
            var (postDtos, response) = await ExecuteWithFullResponseAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/{amount}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDtos.Should().NotBeNull().And.AllBeOfType<PostDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}/error", HttpMethod.Get);

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
            var (postDtos, response) = await ExecuteWithFullResponseAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/user?userId={post.UserId}&amount={amount}",
                HttpMethod.Get);

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
            var (postDtos, response) = await ExecuteWithFullResponseAsync<List<PostDto>>(
                $"{TestingConstants.PostApi}/user?userId={0}&amount={amount}", HttpMethod.Get);

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
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}/user?userId={string.Empty}&amount={amount}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetUserPostsAsync_WhenAmountIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}/user?userId={1}&amount={string.Empty}", 
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var (postDto, response) = 
                await ExecuteWithFullResponseAsync<PostDto>($"{TestingConstants.PostApi}?id={post.Id}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}?id={-1}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}?id={string.Empty}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var postRequestModel = _fixture.Build<CreatePostRequestModel>()
                .With(x => x.UserId, user.Id).Create();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var (postDto, response) = await ExecuteWithFullResponseAsync<PostDto>(
                TestingConstants.PostApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto?.UserId.Should().Be(postRequestModel.UserId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var postRequestModel = _fixture.Build<CreatePostRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            var requestBody = BuildRequestBody(postRequestModel);


            //Act
            var response = await ExecuteWithStatusCodeAsync(
                TestingConstants.PostApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            var postRequestModel = _fixture.Create<UpdatePostRequestModel>();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var (postDto, response) = await ExecuteWithFullResponseAsync<PostDto>(
                $"{TestingConstants.PostApi}?id={post.Id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto?.UserId.Should().Be(post.UserId);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var postRequestModel = _fixture.Create<UpdatePostRequestModel>();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}?id={0}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var postRequestModel = _fixture.Build<UpdatePostRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.PostApi}?id={id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var response = 
                await ExecuteWithStatusCodeAsync($"{TestingConstants.PostApi}?id={post.Id}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = 
                await ExecuteWithStatusCodeAsync($"{TestingConstants.PostApi}?id={0}", HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.PostApi}?id={string.Empty}",
                HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
