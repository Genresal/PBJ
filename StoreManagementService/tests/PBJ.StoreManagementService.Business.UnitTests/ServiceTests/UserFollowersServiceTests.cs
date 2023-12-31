﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations;
using PBJ.StoreManagementService.Business.UnitTests.ServiceTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.UserFollowers;
using System.Linq.Expressions;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.ServiceTests
{
    public class UserFollowersServiceTests : BaseServiceTests
    {
        private readonly Mock<IUserFollowersRepository> _mockUserFollowersRepository;

        public UserFollowersServiceTests()
        {
            _mockUserFollowersRepository = new Mock<IUserFollowersRepository>();
        }

        [Theory]
        [AutoMockData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsListOfDto(
            int page,
            int take,
            PaginationResponse<UserFollowers> response,
            PaginationResponseDto<UserFollowersDto> responseDto)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x =>
                    x.GetPaginatedAsync(It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<Expression<Func<UserFollowers, bool>>>(),
                        It.IsAny<Expression<Func<UserFollowers, int>>>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(response);

            _mockMapper.Setup(x =>
                    x.Map<PaginationResponseDto<UserFollowersDto>>(It.IsAny<PaginationResponse<UserFollowers>>()))
                .Returns(responseDto);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userFollowersService.GetPaginatedAsync(page, take);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.GetPaginatedAsync(It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<UserFollowers, bool>>>(),
                    It.IsAny<Expression<Func<UserFollowers, int>>>(),
                    It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserFollowersDto>>();
        }

        [Theory]
        [AutoMockData]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(
            string userEmail,
            string followerEmail,
            UserFollowers userFollowers,
            UserFollowersDto userFollowersDto)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserFollowers, bool>>>()))
                .ReturnsAsync(userFollowers);

            _mockMapper.Setup(x => x.Map<UserFollowersDto>(userFollowers)).Returns(userFollowersDto);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userFollowersService.GetAsync(userEmail, followerEmail);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserFollowers, bool>>>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserFollowersDto>();
        }

        [Theory]
        [AutoMockData]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException(
            string userEmail,
            string followerEmail)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserFollowers, bool>>>()))
                .ReturnsAsync(value: null);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userFollowersService.GetAsync(userEmail, followerEmail);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserFollowersRepository
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<UserFollowers, bool>>>()), Times.Once);
        }

        [Theory]
        [AutoMockData]
        public async Task CreateAsync_WhenRequestIsValid_ReturnsCreatedDto(UserFollowers userFollowers,
            UserFollowersDto userFollowersDto,
            UserFollowersRequestModel userFollowersRequestModel)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.CreateAsync(It.IsAny<UserFollowers>()));

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            _mockMapper.Setup(x => x.Map<UserFollowers>(userFollowersRequestModel))
                .Returns(userFollowers);
            _mockMapper.Setup(x => x.Map<UserFollowersDto>(userFollowers))
                .Returns(userFollowersDto);

            //Act
            var result = await userFollowersService
                .CreateAsync(userFollowersRequestModel);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.CreateAsync(It.IsAny<UserFollowers>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserFollowersDto>();
        }

        [Theory]
        [AutoMockData]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(
            UserFollowersRequestModel requestModel)
        {
            //Arrange
            _mockUserFollowersRepository
                .Setup(x => x.DeleteAsync(It.IsAny<UserFollowers>()));

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userFollowersService.DeleteAsync(requestModel);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.DeleteAsync(It.IsAny<UserFollowers>()), Times.Once);

            result.Should().BeTrue();
        }

        [Theory]
        [AutoMockData]
        public async Task DeleteAsync_WhenEntityNotExists_ThrowsDbUpdateExceptionException(
            UserFollowersRequestModel requestModel)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x =>
                x.DeleteAsync(It.IsAny<UserFollowers>())).Throws<DbUpdateException>();

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userFollowersService.DeleteAsync(requestModel);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();

            _mockUserFollowersRepository
                .Verify(x => x.DeleteAsync(It.IsAny<UserFollowers>()), Times.Once);
        }
    }
}
