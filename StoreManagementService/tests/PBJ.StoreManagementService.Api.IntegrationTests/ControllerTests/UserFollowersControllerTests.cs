using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.UserFollowers;
using System.Net;
using System.Net.Http.Headers;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserFollowersControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserFollowersApi}/{amount}");

            var userFollowersDtos = JsonConvert.DeserializeObject<List<UserFollowers>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userFollowersDtos.Should().NotBeNull().And.AllBeOfType<UserFollowers>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserFollowersApi}/error");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsDto()
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserFollowersApi}?id={userFollower.Id}");

            var postDto = JsonConvert.DeserializeObject<UserFollowersDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            postDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserFollowersApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserFollowersApi}?id={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsCreatedDto()
        {
            //Arrange
            var user1 = await _dataManager.CreateUserAsync();
            var user2 = await _dataManager.CreateUserAsync();

            var userFollowersRequestModel = _fixture.Build<UserFollowers>()
                .With(x => x.UserId, user1.Id)
                .With(x => x.FollowerId, user2.Id)
                .Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userFollowersRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.UserFollowersApi, requestBody);

            var commentDto = JsonConvert
                .DeserializeObject<UserFollowersDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto.UserId.Should().Be(userFollowersRequestModel.UserId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userFollowersRequestModel = _fixture.Build<UserFollowersRequestModel>()
                .With(x => x.UserId, 0).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userFollowersRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.UserFollowersApi, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue()
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
    }
}
