using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBJ.AuthService.Api.RequestModels;
using PBJ.AuthService.Business.Services.Abstract;
using static Duende.IdentityServer.IdentityServerConstants;

namespace PBJ.AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserAsync(string email)
        {
            var result = await _userService.GetUserAsync(email);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(new
            {
                result.Result.Email,
                result.Result.UserName,
                result.Result.Surname,
                result.Result.BirthDate
            });
        }

        [HttpPut]
        public async Task<ActionResult> EditUserAsync(string email, [FromBody] EditUserRequestModel requestModel)
        {
            var result = await _userService.EditUserAsync(email, 
                requestModel.UserName, requestModel.Surname, requestModel.BirthDate);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [HttpPut, Route("password")]
        public async Task<ActionResult> EditPasswordAsync(string email, EditPasswordRequestModel requestModel)
        {
            var result = await _userService.EditPasswordAsync(email,
                requestModel.CurrentPassword, requestModel.NewPassword);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Result);
        }
    }
}
