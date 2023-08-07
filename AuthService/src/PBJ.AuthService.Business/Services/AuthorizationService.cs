using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.Business.Services.Abstract;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Business.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IUserService _userService;

        public AuthorizationService(SignInManager<AuthUser> signInManager,
            IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var user = await _userService.GetUserAsync(email);

            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public async Task<SignInResult> RegisterAsync(string username, string email, string password)
        {
            var user = await _userService.CreateUserAsync(username, email, password);

            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }
    }
}
