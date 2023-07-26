﻿using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.UnitTests.ServiceTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.User;
using System.Linq.Expressions;
using PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.ServiceTests
{
    public class UserServiceTests : BaseServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserFollowersRepository> _mockUserFollowersRepository;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserFollowersRepository = new Mock<IUserFollowersRepository>();
        }

        [Theory, AutoMockData]
        public async Task GetAmountAsync_WhenEntitiesExists_ReturnsListOfDto(int amount,
            List<User> users,
            List<UserDto> userDtos)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(users);

            _mockMapper.Setup(x => x.Map<List<UserDto>>(users)).Returns(userDtos);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetAmountAsync(amount);

            //Assert
            _mockUserRepository.Verify(x => x.GetAmountAsync(It.IsAny<int>()), Times.Once);

            result.Should().BeOfType<List<UserDto>>().And.AllBeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task GetFollowersAsync_WhenRequestIsValid_ReturnsListOfDto(int userId, 
            int amount,
            List<User> users,
            List<UserDto> userDtos)
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.GetFollowersAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(users);

            _mockMapper.Setup(x => x.Map<List<UserDto>>(users)).Returns(userDtos);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetFollowersAsync(userId, amount);

            //Assert
            _mockUserRepository.Verify(x => x.GetFollowersAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<List<UserDto>>().And.AllBeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task GetFollowingsAsync_WhenRequestIsValid_ReturnsListOfDto(int followerId, 
            int amount,
            List<User> users,
            List<UserDto> userDtos)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetFollowingsAsync(It.IsAny<int>(),
                    It.IsAny<int>())).ReturnsAsync(users);

            _mockMapper.Setup(x => x.Map<List<UserDto>>(users)).Returns(userDtos);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetFollowingsAsync(followerId, amount);

            //Assert
            _mockUserRepository.Verify(x => 
                x.GetFollowingsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<List<UserDto>>().And.AllBeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id,
            User user,
            UserDto userDto)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(user);

            _mockMapper.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetAsync(id);

            //Assert
            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException(int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task CreateAsync_WhenEntityNotExists_ReturnsCreatedDto(
            User user,
            UserDto userDto,
            UserRequestModel userRequestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.CreateAsync(It.IsAny<User>()));

            _mockMapper.Setup(x => x.Map<User>(userRequestModel)).Returns(user);
            _mockMapper.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.CreateAsync(userRequestModel);

            //Assert
            _mockUserRepository.Verify(x =>
                x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _mockUserRepository.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task CreateAsync_WhenEntityExists_ThrowsAlreadyExistsException(
            User user,
            UserRequestModel userRequestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            _mockMapper.Setup(x => x.Map<User>(userRequestModel)).Returns(user);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.CreateAsync(userRequestModel);

            //Assert
            await act.Should().ThrowAsync<AlreadyExistsException>();

            _mockUserRepository.Verify(x =>
                x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto(int id,
            User user,
            UserDto userDto,
            UserRequestModel userRequestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(user);

            _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()));


            _mockMapper.Setup(x => x.Map<User>(userRequestModel)).Returns(user);
            _mockMapper.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.UpdateAsync(id, userRequestModel);

            //Assert
            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task UpdateAsync_WhenEntityNotExists_ThrowsNotFoundException(int id,
            UserRequestModel userRequestModel)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.UpdateAsync(id, userRequestModel);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id,
            User user)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x =>
                x.DeleteRangeAsync(It.IsAny<ICollection<UserFollowers>>()));

            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(user);

            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()));

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.DeleteAsync(id);

            //Assert
            _mockUserRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
            _mockUserRepository.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
            _mockUserFollowersRepository.Verify(x => 
                x.DeleteRangeAsync(It.IsAny<List<UserFollowers>>()), Times.AtLeast(2));

            result.Should().BeTrue();
        }

        [Theory, AutoMockData]
        public async Task DeleteAsync_WhenEntityNotExists_ThrowsDbUpdateExceptionException(int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .Throws<DbUpdateException>();

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();

            _mockUserRepository.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
