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

        [HttpGet, Route("paginated")]
        public async Task<ActionResult> GetPaginatedAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userService
                .GetPaginatedAsync(requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet, Route("followers")]
        public async Task<ActionResult> GetFollowersAsync(string userEmail, 
            [FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userService
                .GetFollowersAsync(userEmail, requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet, Route("followings")]
        public async Task<ActionResult> GetFollowingsAsync(string followerEmail, 
            [FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userService
                .GetFollowingsAsync(followerEmail, requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet, Route("email")]
        public async Task<ActionResult> GetAsync(string email)
        {
            var result = await _userService.GetAsync(email);

            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(UserRequestModel requestModel)
        {
            var result = await _userService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, UserRequestModel requestModel)
        {
            var result = await _userService.UpdateAsync(id, requestModel);

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
