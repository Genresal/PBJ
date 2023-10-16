using AutoFixture;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.Api.IntegrationTests.Handlers;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.UserFollowers;
using System.Net;
using Bogus;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserFollowersControllerTests : BaseControllerTest
    {
        [Theory]
        [CustomAutoData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) =
                await ExecuteWithFullResponseAsync<PaginationResponseDto<UserFollowersDto>>(
                    $"{ApiConstants.UserFollowersApi}/paginated?page={requestModel.Page}&take={requestModel.Take}",
                    HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserFollowersDto>>();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetPaginatedAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserFollowersApi}/paginated?page={requestModel.Page}&take={requestModel.Take}",
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
                $"{ApiConstants.UserFollowersApi}/paginated?page={string.Empty}&take={string.Empty}",
                HttpMethod.Get, token: JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (userFollowerDto, response) = await ExecuteWithFullResponseAsync<UserFollowersDto>(
                $"{ApiConstants.UserFollowersApi}?userEmail={userFollower.UserEmail}&followerEmail={userFollower.FollowerEmail}", 
                HttpMethod.Get, token: JwtTokenHandler.AdminToken);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowerDto.Should().NotBeNull();
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenTokenIsEmpty_ReturnsUnauthorized(
            string userEmail,
            string followerEmail)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserFollowersApi}?userEmail={userEmail}&followerEmail={followerEmail}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound(
            string userEmail,
            string followerEmail)
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{ApiConstants.UserFollowersApi}?userEmail={userEmail}&followerEmail={followerEmail}", HttpMethod.Get, token: JwtTokenHandler.AdminToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk()
        {
            //Arrange
            var user1 = await _dataManager.CreateUserAsync();
            var user2 = await _dataManager.CreateUserAsync();

            var userFollowersRequestModel = _fixture.Build<UserFollowersRequestModel>()
                .With(x => x.UserEmail, user1.Email)
                .With(x => x.FollowerEmail, user2.Email)
                .Create();

            var requestBody = BuildRequestBody(userFollowersRequestModel);

            //Act
            var (userFollowerDto, response) = await ExecuteWithFullResponseAsync<UserFollowersDto>(
                ApiConstants.UserFollowersApi, HttpMethod.Post, requestBody, JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowerDto?.UserEmail.Should().Be(userFollowersRequestModel.UserEmail);
        }

        [Fact]
        public async Task CreateAsync_WhenTokenIsEmpty_ReturnsUnauthorized()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.UserFollowersApi, HttpMethod.Post);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userFollowersRequestModel = _fixture.Build<UserFollowersRequestModel>()
                .With(x => x.UserEmail, "").Create();

            var requestBody = BuildRequestBody(userFollowersRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                ApiConstants.UserFollowersApi, HttpMethod.Post, requestBody, JwtTokenHandler.UserToken);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            var requestBody = BuildRequestBody(new UserFollowersRequestModel()
            {
                UserEmail = userFollower.UserEmail,
                FollowerEmail = userFollower.FollowerEmail
            });

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.UserFollowersApi}?userEmail={userFollower.UserEmail}&followerEmail={userFollower.FollowerEmail}",
                    HttpMethod.Delete, token: JwtTokenHandler.UserToken, requestBody: requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_WhenTokenIsEmpty_ReturnsUnauthorized(int id)
        {
            //Arrange
            var faker = new Faker();

            var requestBody = BuildRequestBody(new UserFollowersRequestModel()
            {
                UserEmail = faker.Internet.Email(),
                FollowerEmail = faker.Internet.Email()
            });

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.UserFollowersApi}",
                    HttpMethod.Delete, requestBody: requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            var faker = new Faker();

            var requestBody = BuildRequestBody(new UserFollowersRequestModel()
            {
                UserEmail = faker.Internet.Email(),
                FollowerEmail = faker.Internet.Email()
            });

            //Act
            var response =
            await ExecuteWithStatusCodeAsync($"{ApiConstants.UserFollowersApi}?userEmail={0}&followerEmail={0}",
                    HttpMethod.Delete, token: JwtTokenHandler.UserToken, requestBody: requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var requestBody = BuildRequestBody(new UserFollowersRequestModel()
            {
                UserEmail = string.Empty,
                FollowerEmail = string.Empty
            });

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{ApiConstants.UserFollowersApi}",
                    HttpMethod.Delete, token: JwtTokenHandler.UserToken, requestBody: requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
