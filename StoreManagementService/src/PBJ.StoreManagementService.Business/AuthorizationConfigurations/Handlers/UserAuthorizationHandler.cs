using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Enums;
using PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.Models.User;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PBJ.StoreManagementService.Business.AuthorizationConfigurations.Handlers
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserRequirement>
    {
        private readonly IUserService _userService;

        public UserAuthorizationHandler(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserService>();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var token = (context.Resource as HttpContext)?.Request.Headers.Authorization.ToString().Replace("Bearer", "")
                .Trim();

            if (string.IsNullOrWhiteSpace(token)) throw new UnauthorizedAccessException("Token cannot be null!");

            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            if (jwtSecurityToken.Claims.First(x => x.Issuer == requirement.Issuer) == null) return Task.CompletedTask;

            var userName = jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Name)?.Value;
            var email = jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Email)?.Value;
            var role = jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(email)) return Task.CompletedTask;

            CheckUserAsync(email!, userName!).GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(role) && role == Role.Admin.ToString())
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            if (requirement.Role == role) context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private async Task CheckUserAsync(string email, string userName)
        {
            try
            {
                await _userService.CreateAsync(new UserRequestModel
                {
                    Email = email
                });
            }
            catch (Exception ex)
            {
                Log.Information("Authorization handler throws exception with message: {message}", ex.Message);
            }
        }
    }
}
