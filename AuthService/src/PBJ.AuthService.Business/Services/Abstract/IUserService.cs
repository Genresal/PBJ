using PBJ.AuthService.Business.Results;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<AuthResult<AuthUser>> GetUserAsync(string email);

        Task<AuthResult<AuthUser>> CreateUserAsync(string userName, string surname, DateTime birthDate, string email, string password);

        Task<AuthResult<AuthUser>> EditUserAsync(string email, string userName, string surname, DateTime birthDate);

        Task<AuthResult<AuthUser>> EditPasswordAsync(string email, string currentPassword, string newPassword);
    }
}
