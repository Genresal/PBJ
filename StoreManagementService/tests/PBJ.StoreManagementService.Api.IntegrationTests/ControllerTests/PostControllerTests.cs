using AutoFixture;
using AutoFixture.Xunit2;
using Bogus;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.Api.IntegrationTests.Handlers;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.Post;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class PostControllerTests : BaseControllerTest
    {
        [Theory]
        [CustomAutoData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) =
                await ExecuteWithFullResponseAsync<PaginationResponseDto<PostDto>>(
                    $"{ApiConstants.PostApi}/paginated?page={requestModel.Page}&take={requestModel.Take}", HttpMethod.Get,
                    token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto.Should().NotBeNull();
            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PostDto>>();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetPaginatedAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}/paginated?page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetPaginatedAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}/paginated?page={0}&take={string.Empty}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetByUserEmailAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<PostDto>>(
                $"{ApiConstants.PostApi}/email?email={post.UserEmail}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PostDto>>();
            paginationResponseDto?.Items.Should().AllSatisfy(x => x.UserEmail.Should().Be(post.UserEmail));
        }

        [Theory]
        [CustomAutoData]
        public async Task GetByUserEmailAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}/email?email={new Faker().Internet.Email()}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetByUserEmailAsync_WhenUserEmailIsInvalid_ReturnsOK(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<PostDto>>(
                $"{ApiConstants.PostApi}/email?email={new Faker().Internet.Email()}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact]
        public async Task GetByUserEmailAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}/email?email={new Faker().Internet.Email()}&page={0}&take={string.Empty}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

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
                await ExecuteWithFullResponseAsync<PostDto>($"{ApiConstants.PostApi}?id={post.Id}",
                    HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.Should().NotBeNull();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenTokenIsEmpty_ReturnsUnauthorized(int id)
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.PostApi}?id={id}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}?id={-1}", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}?id={string.Empty}", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var postRequestModel = _fixture.Build<CreatePostRequestModel>()
                .With(x => x.UserEmail, user.Email).Create();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var (postDto, response) = await ExecuteWithFullResponseAsync<PostDto>(
                ApiConstants.PostApi, HttpMethod.Post, requestBody, JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto?.UserEmail.Should().Be(postRequestModel.UserEmail);
        }

        [Fact]
        public async Task CreateAsync_WhenTokenIsEmpty_ReturnsUnauthorized()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.PostApi, HttpMethod.Post);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
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
                ApiConstants.PostApi, HttpMethod.Post, requestBody, JwtTokenHandler.UserToken);

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
                $"{ApiConstants.PostApi}?id={post.Id}", HttpMethod.Put, requestBody, JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto?.Content.Should().NotBe(post.Content);
        }

        [Theory]
        [CustomAutoData]
        public async Task UpdateAsync_WhenTokenIsEmpty_ReturnsUnauthorized(int id)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}?id={id}", HttpMethod.Put);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var postRequestModel = _fixture.Create<UpdatePostRequestModel>();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}?id={0}", HttpMethod.Put, requestBody, JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var postRequestModel = _fixture.Build<UpdatePostRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            var requestBody = BuildRequestBody(postRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.PostApi}?id={id}", HttpMethod.Put, requestBody, JwtTokenHandler.UserToken);

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
                await ExecuteWithStatusCodeAsync($"{ApiConstants.PostApi}?id={post.Id}",
                    HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenTokenIsEmpty_ReturnsUnauthorized(int id)
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.PostApi}?id={id}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.PostApi}?id={0}", HttpMethod.Delete,
                    token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.PostApi}?id={string.Empty}",
                HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
