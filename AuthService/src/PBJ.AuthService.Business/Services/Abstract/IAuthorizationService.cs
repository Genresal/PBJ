using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.Business.Results;

namespace PBJ.AuthService.Business.Services.Abstract
{
    public interface IAuthorizationService
    {
        Task<AuthResult<SignInResult>> LoginAsync(string email, string password);

        Task<AuthResult<SignInResult>> RegisterAsync(string username, string email, string password);

        Task<LogoutRequest> LogoutAsync(string logoutId);
    }
}
