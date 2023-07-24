using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Mappers;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.User;
using System.Linq.Expressions;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.UserTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserFollowersRepository> _mockUserFollowersRepository;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserFollowersRepository = new Mock<IUserFollowersRepository>();

            _mapper = new Mapper(new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<BusinessProfile>();
            }));

            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Theory]
        [InlineData(2)]
        public async Task GetAmountAsync_WhenEntitiesExists_ReturnsListOfDto(int amount)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<User>(amount).ToList());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.GetAmountAsync(amount);

            //Assert
            result.Count.Should().NotBe(0);
            result.Should().BeOfType<List<UserDto>>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenEntitiesNotExists_ReturnsEmptyListOfDto()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<User>(0).ToList());

            var commentService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await commentService.GetAmountAsync(2);

            //Assert
            result.Count.Should().Be(0);
            result.Should().BeOfType<List<UserDto>>();
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task GetFollowersAsync_WhenEntitiesExists_ReturnsListOfDto(int userId, int amount)
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.GetFollowersAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<User>()
                    .With(x => x.Followers, _fixture.Build<UserFollowers>()
                        .With(x => x.UserId, userId)
                        .CreateMany(2).ToList())
                    .CreateMany(amount).ToList());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.GetFollowersAsync(userId, amount);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<UserDto>>();
            result.Count.Should().NotBe(0);
            result.ForEach(x => x.Followers.ToList()
                .ForEach(x => x.UserId.Should().Be(userId)));
        }

        [Fact]
        public async Task GetFollowersAsync_WhenEntitiesNotExists_ReturnsEmptyListOfDto()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetFollowersAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<User>(0).ToList());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.GetFollowersAsync(1, 2);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<UserDto>>();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetFollowingsAsync_WhenEntitiesNotExists_ReturnsEmptyListOfDto()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetFollowersAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<User>(0).ToList());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.GetFollowersAsync(1, 2);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<UserDto>>();
            result.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task GetFollowingsAsync_WhenEntitiesExists_ReturnsListOfDto(int followerId, int amount)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetFollowingsAsync(It.IsAny<int>(),
                    It.IsAny<int>())).ReturnsAsync(_fixture.Build<User>()
                    .With(x => x.Followings, _fixture.Build<UserFollowers>()
                    .With(x => x.FollowerId, followerId).CreateMany(2).ToList())
                    .CreateMany(amount).ToList());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.GetFollowingsAsync(followerId, amount);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<UserDto>>();
            result.Count.Should().NotBe(0);
            result.ForEach(x => x.Followings.ToList()
                .ForEach(x => x.FollowerId.Should().Be(followerId)));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<User>().With(x => x.Id, id).Create());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.GetAsync(id);

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var act = () => userService.GetAsync(1);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateAsync_WhenEntityNotExists_ReturnsCreatedDto(int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<User>().With(x => x.Id, id).Create());

            _mockUserRepository.Setup(x => x.CreateAsync(It.IsAny<User>()));

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService
                .CreateAsync(_fixture.Create<UserRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExists_ThrowsAlreadyExistsException()
        {
            //Arrange
            _mockUserRepository.Setup(x =>
                    x.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(_fixture.Create<User>());

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var act = () => userService
                .CreateAsync(_fixture.Create<UserRequestModel>());

            //Assert
            await act.Should().ThrowAsync<AlreadyExistsException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto(int id)
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<User>());

            _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()));

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.UpdateAsync(id,
                _fixture.Create<UserRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserDto>();
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var commentService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var act = () => commentService.UpdateAsync(1,
                _fixture.Create<UserRequestModel>());

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x =>
                x.DeleteRangeAsync(It.IsAny<ICollection<UserFollowers>>()));

            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<User>());

            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()));

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userService.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ThrowsDbUpdateExceptionException()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .Throws<DbUpdateException>();

            var userService = new UserService(_mockUserRepository.Object,
                _mockUserFollowersRepository.Object, _mapper);

            //Act
            var act = () => userService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
