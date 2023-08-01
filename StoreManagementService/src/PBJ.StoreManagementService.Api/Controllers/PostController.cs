using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet, Route("{amount}")]
        public async Task<ActionResult> GetAmountAsync(int amount)
        {
            var result = await _postService.GetAmountAsync(amount);

            return Ok(result);
        }

        [HttpGet, Route("user")]
        public async Task<ActionResult> GetUserPostsAsync(int userId, int amount)
        {
            var result = await _postService.GetUserPostsAsync(userId, amount);

            return Ok(result);
        }

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
