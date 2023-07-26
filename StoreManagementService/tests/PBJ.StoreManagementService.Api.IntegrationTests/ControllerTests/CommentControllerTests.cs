using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Models.Comment;
using System.Net;
using System.Net.Http.Headers;
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
            var response = await _httpClient.GetAsync($"{TestingConstants.CommentApi}/{amount}");

            var commentDtos = JsonConvert.DeserializeObject<List<CommentDto>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDtos.Should().NotBeNull().And.AllBeOfType<CommentDto>();
        }

        [Fact]
        public async Task GetAmountAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.CommentApi}/error");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsDto()
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.CommentApi}?id={comment.Id}");

            var commentDto = JsonConvert.DeserializeObject<CommentDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.CommentApi}?id={0}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"{TestingConstants.CommentApi}?id={string.Empty}");

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

            var requestBody = new StringContent(JsonConvert.SerializeObject(commentRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.CommentApi, requestBody);

            var commentDto = JsonConvert.DeserializeObject<CommentDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto.UserId.Should().Be(commentRequestModel.UserId);
            commentDto.PostId.Should().Be(commentRequestModel.PostId);
        }

        [Fact]
        public async Task CreateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var commentRequestModel = _fixture.Build<CommentRequestModel>()
                .With(x => x.Content, string.Empty).Create();

            var requestBody = new StringContent(JsonConvert.SerializeObject(commentRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PostAsync(TestingConstants.CommentApi, requestBody);

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

            var requestBody = new StringContent(JsonConvert.SerializeObject(commentRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.CommentApi}?id={comment.Id}", requestBody);

            var commentDto = JsonConvert.DeserializeObject<CommentDto>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto.UserId.Should().Be(commentRequestModel.UserId);
            commentDto.PostId.Should().Be(commentRequestModel.PostId);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var commentRequestModel = _fixture.Create<CommentRequestModel>();

            var requestBody = new StringContent(JsonConvert.SerializeObject(commentRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.CommentApi}?id={0}", requestBody);

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

            var requestBody = new StringContent(JsonConvert.SerializeObject(commentRequestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(TestingConstants.ContentType);

            //Act
            var response = await _httpClient.PutAsync($"{TestingConstants.CommentApi}?id={id}", requestBody);

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
    }
}
