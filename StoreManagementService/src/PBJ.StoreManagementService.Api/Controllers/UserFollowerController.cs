using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [Authorize(Policy = "User")]
    [ApiController]
    [Route("api/userfollower")]
    public class UserFollowerController : ControllerBase
    {
        private readonly IUserFollowersService _userFollowersService;

        public UserFollowerController(IUserFollowersService commentService)
        {
            _userFollowersService = commentService;
        }

        [HttpGet]
        [Route("paginated")]
        public async Task<ActionResult> GetPaginatedAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var result = await _userFollowersService
                .GetPaginatedAsync(requestModel.Page, requestModel.Take);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(string userEmail, string followerEmail)
        {
            var result = await _userFollowersService.GetAsync(userEmail, followerEmail);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(UserFollowersRequestModel requestModel)
        {
            var result = await _userFollowersService.CreateAsync(requestModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(UserFollowersRequestModel requestModel)
        {
            var result = await _userFollowersService.DeleteAsync(requestModel);

            return Ok(result);
        }
    }
}
