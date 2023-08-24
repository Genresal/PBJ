using AutoFixture;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.UserFollowers;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserFollowersControllerTests : BaseControllerTest
    {
        [Theory, CustomAutoData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) =
                await ExecuteWithFullResponseAsync<PaginationResponseDto<UserFollowersDto>>(
                $"{TestingConstants.UserFollowersApi}/paginated?page={requestModel.Page}&take={requestModel.Take}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().NotBeNull().And.
                BeAssignableTo<IEnumerable<UserFollowersDto>>();
        }

        [Fact]
        public async Task GetPaginatedAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserFollowersApi}/paginated?page={string.Empty}&take={string.Empty}",
                HttpMethod.Get);

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
                $"{TestingConstants.UserFollowersApi}?id={userFollower.Id}", HttpMethod.Get);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowerDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserFollowersApi}?id={0}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.UserFollowersApi}?id={string.Empty}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk()
        {
            //Arrange
            var user1 = await _dataManager.CreateUserAsync();
            var user2 = await _dataManager.CreateUserAsync();

            var userFollowersRequestModel = _fixture.Build<UserFollowersRequestModel>()
                .With(x => x.UserId, user1.Id)
                .With(x => x.FollowerId, user2.Id)
                .Create();

            var requestBody = BuildRequestBody(userFollowersRequestModel);

            //Act
            var (userFollowerDto, response) = await ExecuteWithFullResponseAsync<UserFollowersDto>(
                TestingConstants.UserFollowersApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowerDto?.UserId.Should().Be(userFollowersRequestModel.UserId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userFollowersRequestModel = _fixture.Build<UserFollowersRequestModel>()
                .With(x => x.UserId, 0).Create();

            var requestBody = BuildRequestBody(userFollowersRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                TestingConstants.UserFollowersApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{TestingConstants.UserFollowersApi}?id={userFollower.Id}",
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
                await ExecuteWithStatusCodeAsync($"{TestingConstants.UserFollowersApi}?id={0}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{TestingConstants.UserFollowersApi}?id={string.Empty}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
