using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements;

namespace PBJ.StoreManagementService.Business.AuthorizationConfigurations.Handlers
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            var userName = context.User.FindFirst(x => x.Type == ClaimTypes.Name)!.Value;
            var email = context.User.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;

            if (!string.IsNullOrWhiteSpace(userName)
                && !string.IsNullOrWhiteSpace(email) && context.User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var roles = context.User.FindAll(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

            if (!string.IsNullOrWhiteSpace(userName) 
                && !string.IsNullOrWhiteSpace(email) 
                && Enumerable.SequenceEqual(requirement.Roles.OrderBy(x => x), roles.OrderBy(x => x)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
