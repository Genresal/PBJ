using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PBJ.AuthService.Api.RequestModels;
using PBJ.AuthService.Business.Services.Abstract;

namespace PBJ.AuthService.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IValidator<LoginRequestModel> _loginValidator;
        private readonly IValidator<RegisterRequestModel> _registerValidator;

        public AuthController(IAuthorizationService authorizationService,
            IValidator<LoginRequestModel> loginValidator,
            IValidator<RegisterRequestModel> registerValidator)
        {
            _authorizationService = authorizationService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpGet, Route("login")]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginRequestModel { ReturnUrl = returnUrl });
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult> LoginAsync(LoginRequestModel requestModel)
        {
            var validationResult = await _loginValidator.ValidateAsync(requestModel);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(x =>
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(requestModel);
            }

            var authResult = await _authorizationService
                .LoginAsync(requestModel.Email!, requestModel.Password!);

            if (!authResult.Success)
            {
                ModelState.AddModelError(nameof(LoginRequestModel.Password), authResult.ErrorMessage!);

                return View(requestModel);
            }

            return Redirect(requestModel.ReturnUrl!);
        }

        [HttpGet, Route("register")]
        public ActionResult Register(string returnUrl)
        {
            return View(new RegisterRequestModel { ReturnUrl = returnUrl });
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult> Register(RegisterRequestModel requestModel)
        {
            var validationResult = await _registerValidator.ValidateAsync(requestModel);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(x =>
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(requestModel);
            }

            var authResult = await _authorizationService.RegisterAsync(requestModel.UserName, 
                requestModel.Surname, requestModel.BirthDate, requestModel.Email, requestModel.Password);

            if (!authResult.Success)
            {
                ModelState.AddModelError(nameof(LoginRequestModel.Email), authResult.ErrorMessage!);

                return View(new RegisterRequestModel { ReturnUrl = requestModel.ReturnUrl });
            }

            return Redirect(requestModel.ReturnUrl);
        }

        [HttpGet, Route("logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            if (User.Identity!.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return Redirect("http://localhost:3000");
        }
    }
}
