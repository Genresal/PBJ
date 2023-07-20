using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<CommentRequestModel> _validator;

        public CommentController(ICommentService commentService,
            IValidator<CommentRequestModel> validator)
        {
            _commentService = commentService;
            _validator = validator;
        }

        [HttpGet, Route("get/amount")]
        public async Task<ActionResult> GetAmountAsync(int amount)
        {
            var result = await _commentService.GetAmountAsync(amount);

            return Ok(result);
        }

        [HttpGet, Route("get/id")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _commentService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> CreateAsync(CommentRequestModel requestModel)
        {
            await _validator.ValidateAsync(requestModel, options =>
            {
                options.ThrowOnFailures();
            });

            var result = await _commentService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpPut, Route("update")]
        public async Task<ActionResult> UpdateAsync(int id, CommentRequestModel requestModel)
        {
            await _validator.ValidateAsync(requestModel, options =>
            {
                options.ThrowOnFailures();
            });

            var result = await _commentService.UpdateAsync(id, requestModel);

            return Ok(result);
        }

        [HttpDelete, Route("delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _commentService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
