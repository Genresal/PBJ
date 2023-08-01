using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations;
using PBJ.StoreManagementService.Business.UnitTests.ServiceTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.UserFollowers;
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

        [Theory, AutoMockData]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount,
            List<UserFollowers> userFollowers,
            List<UserFollowersDto> userFollowersDtos)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(userFollowers);

            _mockMapper.Setup(x => x.Map<List<UserFollowersDto>>(userFollowers)).Returns(userFollowersDtos);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userFollowersService.GetAmountAsync(amount);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.GetAmountAsync(It.IsAny<int>()), Times.Once);

            result.Should().BeOfType<List<UserFollowersDto>>();
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id, 
            UserFollowers userFollowers,
            UserFollowersDto userFollowersDto)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(userFollowers);

            _mockMapper.Setup(x => x.Map<UserFollowersDto>(userFollowers)).Returns(userFollowersDto);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userFollowersService.GetAsync(id);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserFollowersDto>();
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException(int id)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userFollowersService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserFollowersRepository
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
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

        [Theory, AutoMockData]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id)
        {
            //Arrange
            _mockUserFollowersRepository
                .Setup(x => x.DeleteAsync(It.IsAny<UserFollowers>()));

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userFollowersService.DeleteAsync(id);

            //Assert
            _mockUserFollowersRepository
                .Verify(x => x.DeleteAsync(It.IsAny<UserFollowers>()), Times.Once);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ThrowsDbUpdateExceptionException()
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x =>
                    x.DeleteAsync(It.IsAny<UserFollowers>())).Throws<DbUpdateException>();

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userFollowersService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();

            _mockUserFollowersRepository
                .Verify(x => x.DeleteAsync(It.IsAny<UserFollowers>()), Times.Once);
        }
    }
}
