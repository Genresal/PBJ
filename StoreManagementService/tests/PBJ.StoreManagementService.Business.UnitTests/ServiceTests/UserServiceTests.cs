using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations;
using PBJ.StoreManagementService.Business.UnitTests.ServiceTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.User;
using System.Linq.Expressions;
using PBJ.StoreManagementService.Models.Pagination;
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
        public async Task GetAmountAsync_WhenEntitiesExists_ReturnsListOfDto(
            int page,
            int take,
            PaginationResponse<User> response,
            PaginationResponseDto<UserDto> responseDto)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetPaginatedAsync(It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Expression<Func<User, int>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(response);

            _mockMapper.Setup(x => x.Map<PaginationResponseDto<UserDto>>(
                    It.IsAny<PaginationResponse<User>>()))
                .Returns(responseDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetPaginatedAsync(page, take);

            //Assert
            _mockUserRepository.Verify(x => x.GetPaginatedAsync(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Expression<Func<User, int>>>(),
                It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
        }

        [Theory, AutoMockData]
        public async Task GetFollowersAsync_WhenRequestIsValid_ReturnsListOfDto(
            string userEmail,
            int page,
            int take,
            PaginationResponse<User> response,
            PaginationResponseDto<UserDto> responseDto)
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.GetPaginatedAsync(It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<Expression<Func<User, bool>>>(),
                        It.IsAny<Expression<Func<User, int>>>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(response);

            _mockMapper.Setup(x => x.Map<PaginationResponseDto<UserDto>>(
                It.IsAny<PaginationResponse<User>>())).Returns(responseDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetFollowersAsync(userEmail, page, take);

            //Assert
            _mockUserRepository.Verify(x => x.GetPaginatedAsync(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Expression<Func<User, int>>>(),
                It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
        }

        [Theory, AutoMockData]
        public async Task GetFollowingsAsync_WhenRequestIsValid_ReturnsListOfDto(
            string followerEmail,
            int page,
            int take,
            PaginationResponse<User> response,
            PaginationResponseDto<UserDto> responseDto)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetPaginatedAsync(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Expression<Func<User, int>>>(),
                It.IsAny<bool>())).ReturnsAsync(response);

            _mockMapper.Setup(x => x.Map<PaginationResponseDto<UserDto>>(
                It.IsAny<PaginationResponse<User>>())).Returns(responseDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetFollowingsAsync(followerEmail, page, take);

            //Assert
            _mockUserRepository.Verify(x =>
                x.GetPaginatedAsync(It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Expression<Func<User, int>>>(),
                    It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserDto>>();
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
        public async Task GetByEmailAsync_WhenEntityExists_ReturnsDto(string email,
            User user,
            UserDto userDto)
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            _mockMapper.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var result = await userService.GetAsync(email);

            //Assert
            _mockUserRepository.Verify(x =>
                x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Theory, AutoMockData]
        public async Task GetByEmailAsync_WhenEntityNotExists_ThrowsNotFoundException(string email)
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value: null);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mockMapper.Object);

            //Act
            var act = () => userService.GetAsync(email);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockUserRepository.Verify(x =>
                x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
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
