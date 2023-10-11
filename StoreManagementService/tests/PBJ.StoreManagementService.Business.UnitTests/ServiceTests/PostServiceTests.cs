using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations;
using PBJ.StoreManagementService.Business.UnitTests.ServiceTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.Post;
using System.Linq.Expressions;
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

        [Theory]
        [AutoMockData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsListOfDto(
            int page,
            int take,
            PaginationResponse<Post> response,
            PaginationResponseDto<PostDto> responseDto)
        {
            //Arrange
            _mockPostRepository.Setup(x => x.GetPaginatedAsync(It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Post, bool>>>(),
                    It.IsAny<Expression<Func<Post, int>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(response);

            _mockMapper.Setup(x => x.Map<PaginationResponseDto<PostDto>>(
                    It.IsAny<PaginationResponse<Post>>()))
                .Returns(responseDto);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.GetPaginatedAsync(page, take);

            //Assert
            _mockPostRepository
                .Verify(x => x.GetPaginatedAsync(It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Post, bool>>>(),
                    It.IsAny<Expression<Func<Post, int>>>(),
                    It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PostDto>>();
        }

        [Theory]
        [AutoMockData]
        public async Task GetByUserEmailAsync_WhenRequestIsValid_ReturnsListOfDto(
            string userEmail,
            int page,
            int take,
            PaginationResponse<Post> response,
            PaginationResponseDto<PostDto> responseDto)
        {
            //Arrange
            _mockPostRepository.Setup(x =>
                    x.GetPaginatedAsync(It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<Expression<Func<Post, bool>>>(),
                        It.IsAny<Expression<Func<Post, int>>>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(response);

            _mockMapper.Setup(x => x.Map<PaginationResponseDto<PostDto>>(
                    It.IsAny<PaginationResponse<Post>>()))
                .Returns(responseDto);

            var postService = new PostService(_mockPostRepository.Object, _mockMapper.Object);

            //Act
            var result = await postService.GetByUserEmailAsync(userEmail, page, take);

            //Assert
            _mockPostRepository.Verify(x =>
                x.GetPaginatedAsync(It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Post, bool>>>(),
                    It.IsAny<Expression<Func<Post, int>>>(),
                    It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();
            result.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PostDto>>();
        }

        [Theory]
        [AutoMockData]
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

        [Theory]
        [AutoMockData]
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

        [Theory]
        [AutoMockData]
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

        [Theory]
        [AutoMockData]
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

        [Theory]
        [AutoMockData]
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
