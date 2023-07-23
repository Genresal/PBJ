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
        public async Task GetAmountAsync_ShouldReturnListOfPostDto(int amount)
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
        public async Task GetAmountAsync_ShouldReturnEmptyListOfPostDto()
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
        public async Task GetUserPostsAsync_ShouldReturnListOfPostDto(int userId, int amount)
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
        public async Task GetUserPostsAsync_ShouldReturnEmptyListOfPostDto()
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
        public async Task GetAsync_ShouldReturnPostDto(int id)
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
        public async Task GetAsync_ShouldThrowNotFoundException()
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
        public async Task CreateAsync_ShouldReturnCreatedPostDto()
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
        public async Task UpdateAsync_ShouldReturnUpdatedPostDto(int id)
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
        public async Task UpdateAsync_ShouldThrowNotFoundException()
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
        public async Task DeleteAsync_ShouldReturnTrue(int id)
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
        public async Task DeleteAsync_ShouldThrowDbUpdateExceptionException()
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
