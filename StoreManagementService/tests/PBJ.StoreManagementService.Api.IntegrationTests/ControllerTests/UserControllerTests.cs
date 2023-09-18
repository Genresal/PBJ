using AutoFixture;
using AutoFixture.Xunit2;
using Bogus;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.User;
using System.Net;
using PBJ.StoreManagementService.Api.IntegrationTests.Handlers;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserControllerTests : BaseControllerTest
    {
        [Theory, CustomAutoData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) = await
                ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                    $"{TestingConstants.UserApi}/paginated?page={requestModel.Page}&take={requestModel.Take}", HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
        }

        [Theory, CustomAutoData]
        public async Task GetPaginatedAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                    $"{TestingConstants.UserApi}/paginated?page={requestModel.Page}&take={requestModel.Take}", 
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
                    $"{TestingConstants.UserApi}/paginated?page={0}&take={string.Empty}", HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, CustomAutoData]
        public async Task GetFollowersAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                $"{TestingConstants.UserApi}/followers?userId={userFollower.UserId}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto.Should().NotBeNull();
            paginationResponseDto?.Items.Should().BeAssignableTo<IEnumerable<UserDto>>();
            paginationResponseDto?.Items.Should().NotBeNull().And.AllSatisfy(x => x.Id.Should().Be(userFollower.FollowerId));
        }

        [Theory, CustomAutoData]
        public async Task GetFollowersAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel,
            int userId)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/followers?userId={userId}&page={requestModel.Page}&take={requestModel.Take}", HttpMethod.Get);

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
                $"{TestingConstants.UserApi}/followers?userId={userFollower.UserId}&page={0}&take={0}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, CustomAutoData]
        public async Task GetFollowersAsync_WhenUserIdIsNotValid_ReturnsBadRequest(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/followers?userId={string.Empty}&page={0}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, CustomAutoData]
        public async Task GetFollowersAsync_WhenUserIdIsZero_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                $"{TestingConstants.UserApi}/followers?userId={0}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeEmpty();
        }

        [Theory, CustomAutoData]
        public async Task GetFollowingsAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                $"{TestingConstants.UserApi}/followings?followerId={userFollower.FollowerId}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
            paginationResponseDto?.Items.Should().NotBeNull().And.AllSatisfy(x => x.Id.Should().Be(userFollower.UserId));
        }

        [Theory, CustomAutoData]
        public async Task GetFollowingsAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel,
            int followerId)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/followings?followerId={followerId}&page={requestModel.Page}&take={requestModel.Take}",
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
                $"{TestingConstants.UserApi}/followings?followerId={userFollower.FollowerId}&page={0}&take={0}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task GetFollowingsAsync_WhenFollowerIdIsNotValid_ReturnsBadRequest(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/followings?followerId={string.Empty}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task GetFollowingsAsync_WhenFollowerIdIsZero_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) = await ExecuteWithFullResponseAsync<PaginationResponseDto<UserDto>>(
                $"{TestingConstants.UserApi}/followings?followerId={0}&page={requestModel.Page}&take={requestModel.Take}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto.Should().NotBeNull();
            paginationResponseDto?.Items.Should().NotBeNull().And.AllBeAssignableTo<IEnumerable<UserDto>>().And.BeEmpty();
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var (userDto, response) =
                await ExecuteWithFullResponseAsync<UserDto>($"{TestingConstants.UserApi}?id={user.Id}",
                    HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Should().NotBeNull();
        }

        [Theory, CustomAutoData]
        public async Task GetAsync_WhenTokenIsEmpty_ReturnsUnauthorized(int id)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.UserApi}?id={id}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetAsync_WhenRoleIsUser_ReturnsForbidden()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.UserApi}?id={user.Id}",
                    HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}?id={0}", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}?id={string.Empty}", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetByEmailAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var (userDto, response) = await ExecuteWithFullResponseAsync<UserDto>(
                $"{TestingConstants.UserApi}/email?email={user.Email}", HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Should().NotBeNull();
        }

        [Theory, CustomAutoData]
        public async Task GetByEmailAsync_WhenTokenIsEmpty_ReturnsUnauthorized(string email)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/email?email={email}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetByEmailAsync_WhenNotEntityExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/email?email=@gmail.com", HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GeByEmailAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}/email?email=", HttpMethod.Get, token: JwtTokenHandler.UserToken);

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
                TestingConstants.UserApi, HttpMethod.Post, requestBody, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto?.Email.Should().Be(userRequestModel.Email);
        }

        [Fact]
        public async Task CreateAsync_WhenTokenIsEmpty_ReturnsUnauthorized()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(TestingConstants.UserApi, HttpMethod.Post);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateAsync_WhenRoleIsUser_ReturnsForbidden()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                TestingConstants.UserApi, HttpMethod.Post, token: JwtTokenHandler.UserToken);

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
                TestingConstants.UserApi, HttpMethod.Post, requestBody, token: JwtTokenHandler.AdminToken);

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
                TestingConstants.UserApi, HttpMethod.Post, requestBody, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var (userDto, response) = await ExecuteWithFullResponseAsync<UserDto>(
                $"{TestingConstants.UserApi}?id={user.Id}", HttpMethod.Put, requestBody, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto?.Email.Should().Be(userRequestModel.Email);
        }

        [Theory, CustomAutoData]
        public async Task UpdateAsync_WhenTokenIsEmpty_ReturnsUnauthorized(int id)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}?id={id}", HttpMethod.Put);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}?id={0}", HttpMethod.Put, requestBody, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateAsync_WhenRequestIsNotValid_ReturnsNotFound()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}?id={string.Empty}",
                HttpMethod.Put, requestBody, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, string.Empty).Create();

            var requestBody = BuildRequestBody(userRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserApi}?id={id}", HttpMethod.Put, requestBody, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.UserApi}?id={user.Id}",
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
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.UserApi}?id={user.Id}",
                HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.UserApi}?id={0}",
                HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync($"{TestingConstants.UserApi}?id={string.Empty}",
                HttpMethod.Delete, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
