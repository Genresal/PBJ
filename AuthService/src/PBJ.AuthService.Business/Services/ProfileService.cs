using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Business.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AuthUser> _userManager;

        public ProfileService(UserManager<AuthUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).GetAwaiter().GetResult();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

            context.IssuedClaims.AddRange(claims);

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            return Task.CompletedTask;
        }
    }
}
