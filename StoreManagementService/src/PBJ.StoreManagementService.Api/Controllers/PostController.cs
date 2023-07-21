using FluentValidation;
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
        private readonly IValidator<PostRequestModel> _validator;

        public PostController(IPostService postService,
            IValidator<PostRequestModel> validator)
        {
            _postService = postService;
            _validator = validator;
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
        public async Task<ActionResult> CreateAsync(PostRequestModel requestModel)
        {
            var result = await _postService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, PostRequestModel requestModel)
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
