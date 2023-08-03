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
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Post;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.ServiceTests
{
    public class PostServiceTests : BaseServiceTests
    {
        private readonly Mock<IPostRepository> _mockPostRepository;

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<IPostRepository>();
        }

        [Theory, AutoMockData]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount,
            List<Post> posts,
            List<PostDto> postDtos)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(posts);

            _mockMapper.Setup(x => x.Map<List<PostDto>>(It.IsAny<List<Post>>()))
                .Returns(postDtos);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.GetAmountAsync(amount);

            //Assert
            _mockPostRepository
                .Verify(x => x.GetAmountAsync(It.IsAny<int>()), Times.Once);

            result.Should().BeOfType<List<PostDto>>().And.AllBeOfType<PostDto>();
        }

        [Theory, AutoMockData]
        public async Task GetUserPostsAsync_WhenRequestIsValid_ReturnsListOfDto(int userId,
            int amount,
            List<Post> posts,
            List<PostDto> postDtos)
        {
            //Arrange
            _mockPostRepository.Setup(x =>
                    x.GetUserPostsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(posts);

            _mockMapper.Setup(x => x.Map<List<PostDto>>(It.IsAny<List<Post>>()))
                .Returns(postDtos);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.GetUserPostsAsync(userId, amount);

            //Assert
            _mockPostRepository.Verify(x => 
                    x.GetUserPostsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<List<PostDto>>();
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id,
            Post post,
            PostDto postDto)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(post);

            _mockMapper.Setup(x => x.Map<PostDto>(It.IsAny<Post>()))
                .Returns(postDto);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.GetAsync(id);

            //Assert
            _mockPostRepository
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<PostDto>();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var act = () => postService.GetAsync(1);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockPostRepository
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task CreateAsync_WhenRequestIsValid_ReturnsCreatedDto(Post post,
            CreatePostRequestModel postRequestModel,
            PostDto postDto)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.CreateAsync(It.IsAny<Post>()));

            _mockMapper.Setup(x => x.Map<Post>(postRequestModel)).Returns(post);
            _mockMapper.Setup(x => x.Map<PostDto>(post)).Returns(postDto);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.CreateAsync(postRequestModel);

            //Assert
            _mockPostRepository
                .Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<PostDto>();
        }

        [Theory, AutoMockData]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto(int id,
            Post post,
            PostDto postDto,
            UpdatePostRequestModel postRequestModel)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(post);

            _mockPostRepository.Setup(x => x.UpdateAsync(It.IsAny<Post>()));

            _mockMapper.Setup(x => x.Map<Post>(postRequestModel)).Returns(post);
            _mockMapper.Setup(x => x.Map<PostDto>(post)).Returns(postDto);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.UpdateAsync(id, postRequestModel);

            //Assert
            _mockPostRepository
                .Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<PostDto>();
        }

        [Theory, AutoMockData]
        public async Task UpdateAsync_WhenEntityNotExists_ThrowsNotFoundException(int id,
            UpdatePostRequestModel postRequestModel)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var act = () => postService.UpdateAsync(id, postRequestModel);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockPostRepository
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.DeleteAsync(It.IsAny<Post>()));

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.DeleteAsync(id);

            //Assert
            _mockPostRepository
                .Verify(x => x.DeleteAsync(It.IsAny<Post>()), Times.Once);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ThrowsDbUpdateExceptionException()
        {
            //Arrange
            _mockPostRepository.Setup(x => x.DeleteAsync(It.IsAny<Post>()))
                .Throws<DbUpdateException>();

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var act = () => postService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();

            _mockPostRepository
                .Verify(x => x.DeleteAsync(It.IsAny<Post>()), Times.Once);
        }
    }
}
