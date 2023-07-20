using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [ApiController]
    [Route("api/userfollower")]
    public class UserFollowerController : ControllerBase
    {
        private readonly IUserFollowersService _userFollowersService;
        private readonly IValidator<UserFollowersRequestModel> _validator;

        public UserFollowerController(IUserFollowersService commentService,
            IValidator<UserFollowersRequestModel> validator)
        {
            _userFollowersService = commentService;
            _validator = validator;
        }

        [HttpGet, Route("get/amount")]
        public async Task<ActionResult> GetAmountAsync(int amount)
        {
            var result = await _userFollowersService.GetAmountAsync(amount);

            return Ok(result);
        }

        [HttpGet, Route("get/id")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _userFollowersService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> CreateAsync(UserFollowersRequestModel requestModel)
        {
            await _validator.ValidateAsync(requestModel, options =>
            {
                options.ThrowOnFailures();
            });

            var result = await _userFollowersService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpDelete, Route("delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _userFollowersService.DeleteAsync(id);

            return Ok(result);
        }
    }
}