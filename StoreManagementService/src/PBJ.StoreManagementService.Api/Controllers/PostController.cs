using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [Authorize(Policy = "User")]
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet, Route("paginated")]
        public async Task<ActionResult> GetPaginatedAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _postService
                .GetPaginatedAsync(requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet, Route("userId")]
        public async Task<ActionResult> GetByUserId(int userId, [FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _postService
                .GetByUserIdAsync(userId, requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _postService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreatePostRequestModel requestModel)
        {
            var result = await _postService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, UpdatePostRequestModel requestModel)
        {
            var result = await _postService.UpdateAsync(id, requestModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _postService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
