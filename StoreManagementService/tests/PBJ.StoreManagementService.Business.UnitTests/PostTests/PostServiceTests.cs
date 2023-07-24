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
using PBJ.StoreManagementService.Models.Post;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.PostTests
{
    public class PostServiceTests
    {
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<IPostRepository>();

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
            _mockPostRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<Post>(amount).ToList());

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.GetAmountAsync(amount);

            //Assert
            result.Count.Should().NotBe(0);
            result.Should().BeOfType<List<PostDto>>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenEntitiesNotExists_ReturnsEmptyListOfDto()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<Post>(0).ToList());

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.GetAmountAsync(2);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<PostDto>>();
            result.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task GetUserPostsAsync_WhenEntitiesExists_ReturnsListOfDto(int userId, int amount)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetUserPostsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<Post>().With(x => x.UserId, userId)
                    .CreateMany(amount).ToList());

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.GetUserPostsAsync(userId, amount);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<PostDto>>();
            result.Count.Should().NotBe(0);
            result.ForEach(x => x.UserId.Should().Be(userId));
        }

        [Fact]
        public async Task GetUserPostsAsync_WhenEntitiesNotExists_ReturnsEmptyListOfDto()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetUserPostsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<Post>(0).ToList());

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.GetUserPostsAsync(1, 2);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<PostDto>>();
            result.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<Post>().With(x => x.Id, id).Create());

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.GetAsync(id);

            //Assert
            result.Should().NotBeNull().And.BeOfType<PostDto>();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Post)null);

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var act = () => postService.GetAsync(1);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateAsync_WhenRequestIsValid_ReturnsCreatedDto()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.CreateAsync(It.IsAny<Post>()));

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.CreateAsync(_fixture.Create<PostRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<PostDto>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto(int id)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<Post>());

            _mockPostRepository.Setup(x => x.UpdateAsync(It.IsAny<Post>()));

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.UpdateAsync(id, _fixture.Create<PostRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<PostDto>();
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Post)null);

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var act = () => postService.UpdateAsync(1, _fixture.Create<PostRequestModel>());

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.DeleteAsync(It.IsAny<Post>()));

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var result = await postService.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ThrowsDbUpdateExceptionException()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.DeleteAsync(It.IsAny<Post>()))
                .Throws<DbUpdateException>();

            var postService = new PostService(_mockPostRepository.Object, _mapper);

            //Act
            var act = () => postService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
