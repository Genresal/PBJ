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

        public async Task<AuthResult<AuthUser>> CreateUserAsync(string userName, 
            string surname, 
            DateTime birthDate,
            string email, 
            string password)
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

        public async Task<AuthResult<AuthUser>> EditUserAsync(string email, string userName, string surname, DateTime birthDate)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthResult<AuthUser>
                {
                    Success = false,
                    ErrorMessage = "User not found!"
                };
            }

            user.UserName = userName;
            user.Surname = surname;
            user.BirthDate = birthDate;

            var editResult = await _userManager.UpdateAsync(user);

            if (!editResult.Succeeded)
            {
                return new AuthResult<AuthUser>
                {
                    Success = false,
                    ErrorMessage = "Can't edit user now!"
                };
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var nameClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var surnameClaim = userClaims.FirstOrDefault(x => x.Type == "surname");
            var birthDateClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.DateOfBirth);

            await _userManager.ReplaceClaimAsync(user, nameClaim!, new Claim(ClaimTypes.Name, userName));
            await _userManager.ReplaceClaimAsync(user, surnameClaim!, new Claim("surname", surname));
            await _userManager.ReplaceClaimAsync(user, birthDateClaim!, new Claim(ClaimTypes.DateOfBirth, birthDate.ToShortDateString()));

            return new AuthResult<AuthUser>
            {
                Success = true,
                Result = user
            };
        }

        public async Task<AuthResult<AuthUser>> EditPasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthResult<AuthUser>
                {
                    Success = false,
                    ErrorMessage = "User not found!"
                };
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!changeResult.Succeeded)
            {
                return new AuthResult<AuthUser>
                {
                    Success = false,
                    ErrorMessage = "Can't change password now!"
                };
            }

            return new AuthResult<AuthUser>
            {
                Success = true
            };
        }
    }
}
