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
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.ServiceTests
{
    public class CommentServiceTests : BaseServiceTests
    {
        private readonly Mock<ICommentRepository> _mockCommentRepository;

        public CommentServiceTests()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
        }

        [Theory, AutoMockData]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount,
            List<Comment> comments, List<CommentDto> commentDtos)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(comments);

            _mockMapper.Setup(x => x.Map<List<CommentDto>>(It.IsAny<List<Comment>>()))
                .Returns(commentDtos);

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var result = await commentService.GetAmountAsync(amount);

            //Assert
            _mockCommentRepository
                .Verify(x => x.GetAmountAsync(It.IsAny<int>()), Times.Once());

            result.Should().BeOfType<List<CommentDto>>().And.AllBeOfType<CommentDto>();
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id,
            Comment comment, CommentDto commentDto)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(comment);

            _mockMapper.Setup(x => x.Map<CommentDto>(comment)).Returns(commentDto);

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var result = await commentService.GetAsync(id);

            //Assert
            _mockCommentRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<CommentDto>();
            result.Should().Be(commentDto);
        }

        [Theory, AutoMockData]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException(int id)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var act = () => commentService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockCommentRepository.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task CreateAsync_WhenRequestIsValid_ReturnsCreatedDto(Comment comment,
            CommentDto commentDto,
            CreateCommentRequestModel commentRequestModel)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.CreateAsync(It.IsAny<Comment>()));

            _mockMapper.Setup(x => x.Map<Comment>(commentRequestModel)).Returns(comment);
            _mockMapper.Setup(x => x.Map<CommentDto>(comment)).Returns(commentDto);

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var result = await commentService
                .CreateAsync(commentRequestModel);

            //Assert
            _mockCommentRepository
                .Verify(x => x.CreateAsync(It.IsAny<Comment>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<CommentDto>();
        }

        [Theory, AutoMockData]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto(int id,
            Comment comment,
            CommentDto commentDto,
            UpdateCommentRequestModel commentRequestModel)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(comment);

            _mockCommentRepository.Setup(x => x.UpdateAsync(It.IsAny<Comment>()));

            _mockMapper.Setup(x => x.Map<Comment>(commentRequestModel)).Returns(comment);
            _mockMapper.Setup(x => x.Map<CommentDto>(comment)).Returns(commentDto);

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var result = await commentService.UpdateAsync(id, commentRequestModel);

            //Assert
            _mockCommentRepository
                .Verify(x => x.UpdateAsync(It.IsAny<Comment>()), Times.Once);

            result.Should().NotBeNull().And.BeOfType<CommentDto>();
        }

        [Theory, AutoMockData]
        public async Task UpdateAsync_WhenEntityNotExists_ThrowsNotFoundException(int id,
            UpdateCommentRequestModel commentRequestModel)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(value: null);

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var act = () => commentService.UpdateAsync(id, commentRequestModel);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();

            _mockCommentRepository
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory, AutoMockData]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.DeleteAsync(It.IsAny<Comment>()));

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var result = await commentService.DeleteAsync(id);

            //Assert
            _mockCommentRepository
                .Verify(x => x.DeleteAsync(It.IsAny<Comment>()), Times.Once);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExist_ThrowsDbUpdateExceptionException()
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.DeleteAsync(It.IsAny<Comment>()))
                .Throws<DbUpdateException>();

            var commentService = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);

            //Act
            var act = () => commentService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();

            _mockCommentRepository
                .Verify(x => x.DeleteAsync(It.IsAny<Comment>()), Times.Once);
        }
    }
}
