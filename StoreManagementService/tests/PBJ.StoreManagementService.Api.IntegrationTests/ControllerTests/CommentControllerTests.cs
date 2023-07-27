using AutoFixture;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Models.Comment;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class CommentControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(10)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsListOfDto(int amount)
        {
            //Arrange
            //Act
            var (commentDtos, response) =
                await GetAsync<List<CommentDto>>($"{TestingConstants.CommentApi}/{amount}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDtos.Should().NotBeNull().And.AllBeOfType<CommentDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<List<CommentDto>>(
                $"{TestingConstants.CommentApi}/error", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsDto()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            //Act
            var (commentDto, response) = await
                GetAsync<CommentDto>($"{TestingConstants.CommentApi}?id={comment.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<CommentDto>(
                $"{TestingConstants.CommentApi}?id={0}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var (_, response) = await GetAsync<CommentDto>(
                $"{TestingConstants.CommentApi}?id={string.Empty}", isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsCreatedDto()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.PostId, post.Id)
                .With(x => x.UserId, post.UserId)
                .Create();

            //Act
            var (commentDto, response) = await
                PostAsync<CommentDto, CommentRequestModel>(TestingConstants.CommentApi, commentRequestModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto?.UserId.Should().Be(commentRequestModel.UserId);
            commentDto?.PostId.Should().Be(commentRequestModel.PostId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            //Act
            var (_, response) = await
                PostAsync<CommentDto, CommentRequestModel>(
                    TestingConstants.CommentApi, commentRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsUpdatedDto()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.UserId, comment.UserId)
                .With(x => x.PostId, comment.PostId)
                .Create();

            //Act
            var (commentDto, response) = await PutAsync<CommentDto, CommentRequestModel>(
                $"{TestingConstants.CommentApi}?id={comment.Id}", commentRequestModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto?.UserId.Should().Be(commentRequestModel.UserId);
            commentDto?.PostId.Should().Be(commentRequestModel.PostId);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var commentRequestModel = _fixture.Create<CommentRequestModel>();

            //Act
            var (_, response) = await PutAsync<CommentDto, CommentRequestModel>(
                $"{TestingConstants.CommentApi}?id={0}", commentRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            //Act
            var (_, response) = await PutAsync<CommentDto, CommentRequestModel>(
                $"{TestingConstants.CommentApi}?id={id}", commentRequestModel, isStatusCodeOnly: true);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsTrue()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.CommentApi}?id={comment.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.CommentApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient
                .DeleteAsync($"{TestingConstants.CommentApi}?id={string.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
