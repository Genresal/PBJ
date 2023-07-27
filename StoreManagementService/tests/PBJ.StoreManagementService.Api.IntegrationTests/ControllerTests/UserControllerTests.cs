using AutoFixture;
using Bogus;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Models.User;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class UserControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            //Act
            var (userDtos, response) = await
                GetAsync<List<UserDto>>($"{TestingConstants.UserApi}/{amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.AllBeOfType<UserDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<UserDto>>(
                    $"{TestingConstants.UserApi}/error", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (userDtos, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followers?userId={userFollower.UserId}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.AllBeOfType<UserDto>();
            userDtos?.ForEach(x => x.Id.Should().Be(userFollower.FollowerId));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenUserIdIsNotValid_ReturnsBadRequest(int amount)
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followers?userId={string.Empty}&amount={amount}", 
                isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowersAsync_WhenUserIdIsZero_ReturnsOk(int amount)
        {
            //Arrange
            //Act
            var (userDtos, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followers?userId={0}&amount={amount}");

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
            var (_, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followers?userId={userId}&amount={string.Empty}", 
                isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            var userFollower = await _dataManager.CreateUserFollowerAsync();

            //Act
            var (userDtos, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followings?followerId={userFollower.FollowerId}&amount={amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDtos.Should().NotBeNull().And.AllBeOfType<UserDto>();
            userDtos?.ForEach(x => x.Id.Should().Be(userFollower.UserId));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenFollowerIdIsNotValid_ReturnsBadRequest(int amount)
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followings?followerId={string.Empty}&amount={amount}", 
                isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetFollowingsAsync_WhenFollowerIdIsZero_ReturnsOk(int amount)
        {
            //Arrange
            //Act
            var (userDtos, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followings?followerId={0}&amount={amount}");

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
            var (_, response) = await GetAsync<List<UserDto>>(
                $"{TestingConstants.UserApi}/followings?followerId={followerId}&amount={string.Empty}",
                isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var (userDto, response) = await GetAsync<UserDto>($"{TestingConstants.UserApi}?id={user.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<UserDto>(
                $"{TestingConstants.UserApi}?id={0}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityNotExists_ReturnsOk()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, new Faker().Internet.Email()).Create();

            //Act
            var (userDto, response) = await PostAsync<UserDto, UserRequestModel>(
                TestingConstants.UserApi, userRequestModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto?.Email.Should().Be(userRequestModel.Email);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExists_ReturnsConflict()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, user.Email).Create();

            //Act
            var (_, response) = await PostAsync<UserDto, UserRequestModel>(
                TestingConstants.UserApi, userRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, string.Empty).Create();

            //Act
            var (_, response) = await PostAsync<UserDto, UserRequestModel>(
                TestingConstants.UserApi, userRequestModel, isStatusCodeOnly: true);

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

            //Act
            var (userDto, response) = await PutAsync<UserDto, UserRequestModel>(
                $"{TestingConstants.UserApi}?id={user.Id}", userRequestModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            userDto?.Email.Should().Be(userRequestModel.Email);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            //Act
            var (_, response) = await PutAsync<UserDto, UserRequestModel>(
                $"{TestingConstants.UserApi}?id={0}", userRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateAsync_WhenRequestIsNotValid_ReturnsNotFound()
        {
            //Arrange
            var userRequestModel = _fixture.Build<UserRequestModel>()
                .With(x => x.Email, "testEmail@gmail.com").Create();

            //Act
            var (_, response) = await PutAsync<UserDto, UserRequestModel>(
                $"{TestingConstants.UserApi}?id={string.Empty}", userRequestModel, isStatusCodeOnly: true);

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

            //Act
            var (_, response) = await PutAsync<UserDto, UserRequestModel>(
                $"{TestingConstants.UserApi}?id={id}", userRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var user = await _dataManager.CreateUserAsync();

            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserApi}?id={user.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.UserApi}?id={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
