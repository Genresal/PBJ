using AutoFixture;
using AutoFixture.Xunit2;
using Bogus;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.Api.IntegrationTests.Handlers;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.User;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserControllerTests : BaseControllerTest
    {
        [Theory]
        [CustomAutoData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) = await
                ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                    $"{ApiConstants.UserApi}/paginated?page={requestModel.Page}&take={requestModel.Take}", HttpMethod.Get,
                    token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetPaginatedAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/paginated?page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPaginatedAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/paginated?page={0}&take={string.Empty}", HttpMethod.Get,
                token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetFollowersAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                $"{ApiConstants.UserApi}/followers?email={userFollower.UserEmail}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto.Should().NotBeNull();
            paginationResponseDto?.Items.Should().BeAssignableTo<IEnumerable<UserDto>>();
            paginationResponseDto?.Items.Should().NotBeNull().And
                .AllSatisfy(x => x.Email.Should().Be(userFollower.FollowerEmail));
        }

        [Theory]
        [CustomAutoData]
        public async Task GetFollowersAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel,
            int userId)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followers?email={userId}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetFollowersAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followers?email={userFollower.UserEmail}&page={0}&take={0}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetFollowersAsync_WhenUserEmailIsNotValid_ReturnsBadRequest(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followers?email={string.Empty}&page={0}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetFollowingsAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                $"{ApiConstants.UserApi}/followings?email={userFollower.UserEmail}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
            paginationResponseDto?.Items.Should().NotBeNull().And
                .AllSatisfy(x => x.Email.Should().Be(userFollower.UserEmail));
        }

        [Theory]
        [CustomAutoData]
        public async Task GetFollowingsAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel,
            int followerId)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followings?email={followerId}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetFollowingsAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followings?email={userFollower.FollowerEmail}&page={0}&take={0}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoData]
        public async Task GetFollowingsAsync_WhenFollowerEmailIsNotValid_ReturnsBadRequest(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followings?email={string.Empty}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [AutoData]
        public async Task GetFollowingsAsync_WhenFollowerEmailIsInvalid_ReturnsBadRequest(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/followings?email=email&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var (userDto, response) = await ExecuteWithFullResponseAsync<UserDto>(
                $"{ApiConstants.UserApi}/email?email={user.Email}", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Should().NotBeNull();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenTokenIsEmpty_ReturnsUnauthorized(string email)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/email?email={email}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenTokenIsNotAdmin_ReturnsForbidden(string email)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/email?email={email}", HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task GetAsync_WhenNotEntityExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/email?email=blabla@gmail.com", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserApi}/email?email=", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityNotExists_ReturnsOk()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, new Faker().Internet.Email()).Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var (userDto, response) = await ExecuteWithFullResponseAsync<UserDto>(
                ApiConstants.UserApi, HttpMethod.Post, requestBody, JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto?.Email.Should().Be(userRequestModel.Email);
        }

        [Fact]
        public async Task CreateAsync_WhenTokenIsEmpty_ReturnsUnauthorized()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(ApiConstants.UserApi, HttpMethod.Post);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateAsync_WhenRoleIsUser_ReturnsForbidden()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.UserApi, HttpMethod.Post, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExists_ReturnsConflict()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, user.Email).Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.UserApi, HttpMethod.Post, requestBody, JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, string.Empty).Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.UserApi, HttpMethod.Post, requestBody, JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={user.Id}",
                HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenTokenIsEmpty_ReturnsUnauthorized()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={user.Id}",
                HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={0}",
                HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{ApiConstants.UserApi}?id={string.Empty}",
                HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
