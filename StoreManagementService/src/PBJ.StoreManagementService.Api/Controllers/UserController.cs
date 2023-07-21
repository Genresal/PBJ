using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<UserRequestModel> _validator;

        public UserController(IUserService userService,
            IMapper mapper,
            IValidator<UserRequestModel> validator)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet, Route("{amount}")]
        public async Task<ActionResult> GetAmountAsync(int amount)
        {
            var result = await _userService.GetAmountAsync(amount);

            return Ok(result);
        }

        [HttpGet, Route("followers")]
        public async Task<ActionResult> GetFollowersAsync(int userId, int amount)
        {
            var result = await _userService.GetFollowersAsync(userId, amount);

            return Ok(result);
        }

        [HttpGet, Route("followings")]
        public async Task<ActionResult> GetFollowingsAsync(int followerId, int amount)
        {
            var result = await _userService.GetFollowingsAsync(followerId, amount);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _userService.GetAsync(id);

            return Ok(result);
        }

        [HttpGet, Route("email")]
        public async Task<ActionResult> GetAsync(string email)
        {
            var result = await _userService.GetAsync(email);

            return Ok(result);
        }

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
