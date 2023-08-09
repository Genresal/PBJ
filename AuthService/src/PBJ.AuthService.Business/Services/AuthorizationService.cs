using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.Business.Results;
using PBJ.AuthService.Business.Services.Abstract;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Business.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthorizationService(SignInManager<AuthUser> signInManager,
            IUserService userService,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userService = userService;
            _interactionService = interactionService;
        }

        public async Task<AuthResult<SignInResult>> LoginAsync(string email, string password)
        {
            var userServiceResult = await _userService.GetUserAsync(email);

            if (!userServiceResult.Success)
            {
                return new AuthResult<SignInResult>
                {
                    Success = false,
                    ErrorMessage = userServiceResult.ErrorMessage
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userServiceResult.Result!, password, false, false);

            return new AuthResult<SignInResult>
            {
                Success = true,
                Result = signInResult
            };
        }

        public async Task<AuthResult<SignInResult>> RegisterAsync(string username, string email, string password)
        {
            var userServiceResult = await _userService.CreateUserAsync(username, email, password);

            if (!userServiceResult.Success)
            {
                return new AuthResult<SignInResult>
                {
                    Success = false,
                    ErrorMessage = userServiceResult.ErrorMessage
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userServiceResult.Result!, password, false, false);

            return new AuthResult<SignInResult>
            {
                Success = true,
                Result = signInResult
            };
        }


        public async Task<LogoutRequest> LogoutAsync(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutResult = await _interactionService.GetLogoutContextAsync(logoutId);

            return logoutResult;
        }
    }
}
