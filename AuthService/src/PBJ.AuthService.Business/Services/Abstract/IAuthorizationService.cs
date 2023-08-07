using Microsoft.AspNetCore.Identity;

namespace PBJ.AuthService.Business.Services.Abstract
{
    public interface IAuthorizationService
    {
        Task<SignInResult> LoginAsync(string email, string password);

        Task<SignInResult> RegisterAsync(string username, string email, string password);
    }
}
