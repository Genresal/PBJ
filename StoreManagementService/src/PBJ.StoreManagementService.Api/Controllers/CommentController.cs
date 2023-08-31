using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Pagination;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [Authorize(Policy = "User")]
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet, Route("paginated")]
        public async Task<ActionResult> GetPaginatedAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _commentService
                .GetPaginatedAsync(requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet, Route("postId")]
        public async Task<ActionResult> GetByPostId(int postId, [FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _commentService
                .GetByPostIdAsync(postId, requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [Authorize("Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _commentService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateCommentRequestModel requestModel)
        {
            var result = await _commentService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, UpdateCommentRequestModel requestModel)
        {
            var result = await _commentService.UpdateAsync(id, requestModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _commentService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
