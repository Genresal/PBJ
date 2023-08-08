﻿using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests.Abstract;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Pagination;
using System.Net;
using Xunit;

namespace PBJ.StoreManagementService.Api.IntegrationTests.ControllerTests
{
    public class CommentControllerTests : BaseControllerTest
    {
        [Theory, CustomAutoData]
        public async Task GetPaginatedAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) =
                await ExecuteWithFullResponseAsync<PaginationResponseDto<CommentDto>>(
                $"{TestingConstants.CommentApi}/paginated?page={requestModel.Page}&take={requestModel.Take}", HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<CommentDto>>();
        }

        [Fact]
        public async Task GetPaginatedAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}/paginated?page={string.Empty}&take={string.Empty}",
                HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, CustomAutoData]
        public async Task GetByPostIdAsync_WhenRequestIsValid_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            var comment = await _dataManager.CreateCommentAsync();

            //Act
            var (paginationResponseDto, response) =
                await ExecuteWithFullResponseAsync<PaginationResponseDto<CommentDto>>(
                    $"{TestingConstants.CommentApi}/postId?postId={comment.PostId}&page={requestModel.Page}&take={requestModel.Take}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto?.Items.Should().AllSatisfy(x => x.PostId.Should().Be(comment.PostId));
            paginationResponseDto?.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<CommentDto>>();
        }

        [Theory, CustomAutoData]
        public async Task GetByPostIdAsync_WhenPostIdIsZero_ReturnsOk(
            PaginationRequestModel requestModel)
        {
            //Arrange
            //Act
            var (paginationResponseDto, response) =
                await ExecuteWithFullResponseAsync<PaginationResponseDto<CommentDto>>(
                    $"{TestingConstants.CommentApi}/postId?postId={0}&page={requestModel.Page}&take={requestModel.Take}",
                    HttpMethod.Get);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            paginationResponseDto.Items.Should().NotBeNull().And.BeAssignableTo<IEnumerable<CommentDto>>();
        }

        [Fact]
        public async Task GetByPostIdAsync_WhenRequestIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}/postId?postId={string.Empty}&page={string.Empty}&take={string.Empty}",
                HttpMethod.Get);

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

            var commentRequestModel = _fixture.Build<CreateCommentRequestModel>()
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
            var commentRequestModel = _fixture.Build<CreateCommentRequestModel>()
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

            var commentRequestModel = _fixture.Create<UpdateCommentRequestModel>();

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var (commentDto, response) = await ExecuteWithFullResponseAsync<CommentDto>(
                $"{TestingConstants.CommentApi}?id={comment.Id}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            commentDto?.Content.Should().NotBe(comment.Content);
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityNotExists_ReturnsNotFound()
        {
            //Arrange
            var commentRequestModel = _fixture.Create<UpdateCommentRequestModel>();

            var requestBody = BuildRequestBody(commentRequestModel);

            //Act
            var response = await ExecuteWithStatusCodeAsync(
                $"{TestingConstants.CommentApi}?id={0}", HttpMethod.Put, requestBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task UpdateAsync_WhenRequestModelIsNotValid_ReturnsBadRequest(int id)
        {
            //Arrange
            var commentRequestModel = _fixture.Build<UpdateCommentRequestModel>()
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