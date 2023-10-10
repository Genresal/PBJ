using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.Business.Results;
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

        public async Task<AuthResult<AuthUser>> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthResult<AuthUser>
                {
                    Success = false,
                    ErrorMessage = "The user is not registered yet!"
                };
            }

            return new AuthResult<AuthUser>
            {
                Success = true,
                Result = user
            };
        }

        public async Task<AuthResult<AuthUser>> CreateUserAsync(string userName, string surname, DateTime birthDate, string email, string password)
        {
            var user = new AuthUser
            {
                UserName = userName,
                Surname = surname,
                BirthDate = birthDate,
                Email = email,
            };

            var creationResult = await _userManager.CreateAsync(user, password);

            if (!creationResult.Succeeded)
            {
                return new AuthResult<AuthUser>
                {
                    Success = false,
                    ErrorMessage = creationResult.Errors.First().Description
                };
            }

            await _userManager.AddToRoleAsync(user, Role.User.ToString());

            var role = (await _userManager.GetRolesAsync(user)).First();

            await _userManager.AddClaimsAsync(user, new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("surname", user.Surname),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToShortDateString()),
                new Claim(ClaimTypes.Role, role)
            });

            return new AuthResult<AuthUser>
            {
                Success = true,
                Result = user
            };
        }
    }
}
