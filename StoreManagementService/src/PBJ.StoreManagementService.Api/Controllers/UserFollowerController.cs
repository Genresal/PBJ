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

        [HttpGet, Route("{amount}")]
        public async Task<ActionResult> GetAmountAsync(int amount)
        {
            var result = await _userFollowersService.GetAmountAsync(amount);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _userFollowersService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(UserFollowersRequestModel requestModel)
        {
            var result = await _userFollowersService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _userFollowersService.DeleteAsync(id);

            return Ok(result);
        }
    }
}