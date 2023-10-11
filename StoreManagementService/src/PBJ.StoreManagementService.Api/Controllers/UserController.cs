using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [Authorize(Policy = "User")]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("paginated")]
        public async Task<ActionResult> GetPaginatedAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userService
                .GetPaginatedAsync(requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet]
        [Route("followers")]
        public async Task<ActionResult> GetFollowersAsync([FromQuery] UserRequestModel userRequestModel,
            [FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userService
                .GetFollowersAsync(userRequestModel.Email!, requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet]
        [Route("followings")]
        public async Task<ActionResult> GetFollowingsAsync([FromQuery] UserRequestModel followerRequestModel,
            [FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userService
                .GetFollowingsAsync(followerRequestModel.Email!, requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        [Route("email")]
        public async Task<ActionResult> GetAsync([FromQuery] UserRequestModel requestModel)
        {
            var result = await _userService.GetAsync(requestModel.Email!);

            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(UserRequestModel requestModel)
        {
            var result = await _userService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _userService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
