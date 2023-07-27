using AutoFixture;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Models.UserFollowers;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserFollowersControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            //Act
            var (userFollowersDtos, response) = await GetAsync<List<UserFollowersDto>>(
                $"{TestingConstants.UserFollowersApi}/{amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowersDtos.Should().NotBeNull().And.AllBeOfType<UserFollowersDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<UserFollowersDto>>(
                $"{TestingConstants.UserFollowersApi}/error", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (userFollowerDto, response) = await GetAsync<UserFollowersDto>(
                $"{TestingConstants.UserFollowersApi}?id={userFollower.Id}");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowerDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<UserFollowersDto>(
                $"{TestingConstants.UserFollowersApi}?id={0}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<UserFollowersDto>(
                $"{TestingConstants.UserFollowersApi}?id={string.Empty}", isStatusCodeOnly: true);

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

            //Act
            var (userFollowerDto, response) = await PostAsync<UserFollowersDto, UserFollowersRequestModel>(
                TestingConstants.UserFollowersApi, userFollowersRequestModel);

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

            //Act
            var (_, response) = await PostAsync<UserFollowersDto, UserFollowersRequestModel>(
                TestingConstants.UserFollowersApi, userFollowersRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserFollowersApi}?id={userFollower.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserFollowersApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserFollowersApi}?id={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
