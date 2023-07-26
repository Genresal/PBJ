using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.User;
using System.Net;
using System.Net.Http.Headers;
using Bogus;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserApi}/{amount}");

            var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.AllBeOfType<UserDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserApi}/error");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followers?userId={userFollower.UserId}&amount={amount}");

            var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.AllBeOfType<UserDto>();
            userDtos.ForEach(x => x.Id.Should().Be(userFollower.FollowerId));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenUserIdIsNotValid_ReturnsBadRequest(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followers?userId={string.Empty}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenUserIdIsZero_ReturnsEmptyListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followers?userId={0}&amount={amount}");

            var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.HaveCount(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenAmountIsNotValid_ReturnsBadRequest(int userId)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followers?userId={userId}&amount={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followings?followerId={userFollower.FollowerId}&amount={amount}");

            var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.AllBeOfType<UserDto>();
            userDtos.ForEach(x => x.Id.Should().Be(userFollower.UserId));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenFollowerIdIsNotValid_ReturnsBadRequest(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followings?followerId={string.Empty}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenFollowerIdIsZero_ReturnsEmptyListOfDto(int amount)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followers?userId={0}&amount={amount}");

            var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.HaveCount(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenAmountIsNotValid_ReturnsBadRequest(int followerId)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .GetAsync($"{TestingConstants.UserApi}/followers?userId={followerId}&amount={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsDto()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserApi}?id={user.Id}");

            var userDto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.UserApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityNotExists_ReturnsCreatedDto()
        {
            //Arrange
            var userRequestModel = _fixture.Build<User>()
                .With(x => x.Email, new Faker().Internet.Email()).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.UserApi, requestBody);

            var userDto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Email.Should().Be(userRequestModel.Email);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExists_ReturnsConflict()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, user.Email).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.UserApi, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, string.Empty).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.UserApi, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var userRequestModel = _fixture.Build<User>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.UserApi}?id={user.Id}", requestBody);

            var userDto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Email.Should().Be(userRequestModel.Email);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var userRequestModel = _fixture.Build<User>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.UserApi}?id={0}", requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateAsync_WhenRequestIsNotValid_ReturnsNotFound()
        {
            //Arrange
            var userRequestModel = _fixture.Build<User>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.UserApi}?id={string.Empty}", requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, string.Empty).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(userRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.UserApi}?id={id}", requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserApi}?id={user.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError(int id)
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserApi}?id={id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
