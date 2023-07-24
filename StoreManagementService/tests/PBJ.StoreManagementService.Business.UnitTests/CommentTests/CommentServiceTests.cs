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
using PBJ.StoreManagementService.Models.Comment;
using Xunit;

namespace PBJ.StoreManagementService.Business.UnitTests.CommentTests
{
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _mockCommentRepository;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public CommentServiceTests()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();

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
        public async Task GetAmountAsync_WhenEntitiesExist_ReturnsListOfDto(int amount)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<Comment>(amount).ToList());

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var result = await commentService.GetAmountAsync(amount);

            //Assert
            result.Count.Should().NotBe(0);
            result.Should().BeOfType<List<CommentDto>>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenEntitiesNotExist_ReturnsEmptyListOfDto()
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAmountAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.CreateMany<Comment>(0).ToList());

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var result = await commentService.GetAmountAsync(2);

            //Assert
            result.Should().NotBeNull().And.BeOfType<List<CommentDto>>();
            result.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_WhenEntityExists_ReturnsDto(int id)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Build<Comment>().With(x => x.Id, id).Create());

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var result = await commentService.GetAsync(id);

            //Assert
            result.Should().NotBeNull().And.BeOfType<CommentDto>();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Comment)null);

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var act = () => commentService.GetAsync(1);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateAsync_WhenRequestIsValid_ReturnsCreatedDto()
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.CreateAsync(It.IsAny<Comment>()));

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var result = await commentService
                .CreateAsync(_fixture.Create<CommentRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<CommentDto>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto(int id)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<Comment>());

            _mockCommentRepository.Setup(x => x.UpdateAsync(It.IsAny<Comment>()));

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var result = await commentService.UpdateAsync(id,
                _fixture.Create<CommentRequestModel>());

            //Assert
            result.Should().NotBeNull().And.BeOfType<CommentDto>();
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ThrowsNotFoundException()
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Comment)null);

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var act = () => commentService.UpdateAsync(1,
                _fixture.Create<CommentRequestModel>());

            //Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue(int id)
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.DeleteAsync(It.IsAny<Comment>()));

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var result = await commentService.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExist_ThrowsDbUpdateExceptionException()
        {
            //Arrange
            _mockCommentRepository.Setup(x => x.DeleteAsync(It.IsAny<Comment>()))
                .Throws<DbUpdateException>();

            var commentService = new CommentService(_mockCommentRepository.Object, _mapper);

            //Act
            var act = () => commentService.DeleteAsync(1);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
