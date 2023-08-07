using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<AuthUser> GetUserAsync(string email);

        Task<AuthUser> CreateUserAsync(string userName, string email, string password);
    }
}
