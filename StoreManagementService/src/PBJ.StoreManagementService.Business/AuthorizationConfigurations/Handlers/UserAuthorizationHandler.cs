using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Enums;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using System.Security.Claims;

namespace PBJ.StoreManagementService.Business.AuthorizationConfigurations.Handlers
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserRequirement>
    {
        private readonly IUserRepository _userService;

        public UserAuthorizationHandler(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserRepository>();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            if (context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier && x.Issuer == requirement.Issuer) == null)
            {
                return Task.CompletedTask;
            }

            var userName = context.User.FindFirst(x => x.Type == ClaimTypes.Name)!.Value;
            var email = context.User.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
            var role = context.User.FindFirst(x => x.Type == ClaimTypes.Role)!.Value;

            if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(email))
            {
                return Task.CompletedTask;
            }

            CheckUserAsync(email, userName).GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(role) && role == Role.Admin.ToString())
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            if (requirement.Role == role)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private async Task CheckUserAsync(string email, string userName)
        {
            var user = await _userService.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                await _userService.CreateAsync(new User()
                {
                    Email = email,
                    Name = userName,
                    LastName = string.Empty,
                    Surname = string.Empty,
                });
            }
        }
    }
}
