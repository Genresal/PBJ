using AutoFixture;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.Comment;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class CommentControllerTests : BaseControllerTest
    {
        [Theory]
        [InlineData(10)]
        public async Task GetAmountAsync_WhenRequestIsValid_ReturnsOk(int amount)
        {
            //Arrange
            //Act

            var (commentDtos, response) = await ExecuteWithFullResponseAsync<List<CommentDto>>(
                $"{TestingConstants.CommentApi}/{amount}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDtos.Should().NotBeNull().And.AllBeOfType<CommentDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}/error", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            //Act
            var (commentDto, response) = 
                await ExecuteWithFullResponseAsync<CommentDto>($"{TestingConstants.CommentApi}?id={comment.Id}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = 
                await ExecuteWithStatusCodeAsync($"{TestingConstants.CommentApi}?id={0}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}?id={string.Empty}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsValid_ReturnsOk()
        {
            //Arrange
            var post = await _dataManager.CreatePostAsync();

            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.PostId, post.Id)
                .With(x => x.UserId, post.UserId)
                .Create();

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var (commentDto, response) = await
                ExecuteWithFullResponseAsync<CommentDto>(TestingConstants.CommentApi,
                    HttpMethod.Post, requestBody);

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

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                    TestingConstants.CommentApi, HttpMethod.Post, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.UserId, comment.UserId)
                .With(x => x.PostId, comment.PostId)
                .Create();

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var (commentDto, response) = await ExecuteWithFullResponseAsync<CommentDto>(
                $"{TestingConstants.CommentApi}?id={comment.Id}", HttpMethod.Put, requestBody);

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

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}?id={0}", HttpMethod.Put, requestBody);

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

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}?id={id}", HttpMethod.Put,
                requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExists_ReturnsOk()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{TestingConstants.CommentApi}?id={comment.Id}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityNotExists_ReturnsInternalServerError()
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{TestingConstants.CommentApi}?id={0}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response =
                await ExecuteWithStatusCodeAsync($"{TestingConstants.CommentApi}?id={string.Empty}",
                    HttpMethod.Delete);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
