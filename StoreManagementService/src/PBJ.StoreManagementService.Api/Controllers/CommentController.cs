using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.Comment;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet, Route("{amount}")]
        public async Task<ActionResult> GetAmountAsync(int amount)
        {
            var result = await _commentService.GetAmountAsync(amount);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _commentService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CommentRequestModel requestModel)
        {
            var result = await _commentService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, CommentRequestModel requestModel)
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
