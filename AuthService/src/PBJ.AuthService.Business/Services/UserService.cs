using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.Business.Exceptions;
using PBJ.AuthService.Business.Services.Abstract;
using PBJ.AuthService.DataAccess.Entities;
using PBJ.AuthService.DataAccess.Enums;
using System.Security.Claims;

namespace PBJ.AuthService.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AuthUser> _userManager;

        public UserService(UserManager<AuthUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthUser> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException("User not found!");
            }

            return user;
        }

        public async Task<AuthUser> CreateUserAsync(string userName, string email, string password)
        {
            var user = new AuthUser
            {
                UserName = userName,
                Email = email,
            };

            var creationResult = await _userManager.CreateAsync(user, password);

            if (!creationResult.Succeeded)
            {
                throw new NotFoundException("Bla bla");
            }

            await _userManager.AddToRoleAsync(user, Role.User.ToString());

            var role = (await _userManager.GetRolesAsync(user)).First();

            await _userManager.AddClaimsAsync(user, new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            });

            return user;
        }
    }
}
