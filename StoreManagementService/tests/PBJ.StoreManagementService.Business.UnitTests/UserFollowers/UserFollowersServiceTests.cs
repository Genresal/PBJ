using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Mappers;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.UserFollowers;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.UserFollowers
{
    public class UserFollowersServiceTests
    {
        private readonly Mock<IUserFollowersRepository> _mockUserFollowersRepository;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public UserFollowersServiceTests()
        {
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
        public async Task GetAmountAsync_ShouldReturnListOfCommentDto(int amount)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<UserFollowers>(amount).ToList());

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userFollowersService.GetAmountAsync(amount);

            //Assert
            result.Count.Should().NotBe(0);
            result.Should().BeOfType<List<UserFollowersDto>>();
        }

        [Fact]
        public async Task GetAmountAsync_ShouldReturnEmptyListOfCommentDto()
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<UserFollowers>(0).ToList());

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userFollowersService.GetAmountAsync(2);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<UserFollowersDto>>();
            result.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnCommentDto(int id)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<UserFollowers>()
                    .With(x => x.Id, id).Create());

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userFollowersService.GetAsync(id);

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserFollowersDto>();
        }

        [Fact]
        public async Task GetAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((UserFollowers)null);

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var act = () => userFollowersService.GetAsync(1);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedCommentDto()
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.CreateAsync(It.IsAny<UserFollowers>()));

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userFollowersService
                .CreateAsync(_fixture.Create<UserFollowersRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<UserFollowersDto>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnTrue(int id)
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x => x.DeleteAsync(It.IsAny<UserFollowers>()));

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var result = await userFollowersService.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowDbUpdateExceptionException()
        {
            //Arrange
            _mockUserFollowersRepository.Setup(x =>
                    x.DeleteAsync(It.IsAny<UserFollowers>())).Throws<DbUpdateException>();

            var userFollowersService = new UserFollowersService(_mockUserFollowersRepository.Object, _mapper);

            //Act
            var act = () => userFollowersService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
