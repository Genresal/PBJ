using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PBJ.AuthService.Business.Configurations;
using PBJ.AuthService.DataAccess.Context;
using PBJ.AuthService.DataAccess.Entities;
using PBJ.AuthService.DataAccess.Enums;
using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.Mappers;

namespace PBJ.AuthService.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void InititalizeDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

            var authDbContext = serviceProvider.GetRequiredService<AuthDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<AuthUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AuthRole>>();

            if (!authDbContext.IdentityResources.Any())
            {
                foreach (var identityResource in IdentityConfiguration.GetIdentityResources())
                {
                    authDbContext.IdentityResources.Add(identityResource.ToEntity());
                }

                authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
            }

            //if (!authDbContext.ApiResources.Any())
            //{
            //    foreach (var apiResource in IdentityConfiguration.GetApiResources())
            //    {
            //        authDbContext.ApiResources.Add(apiResource.ToEntity());
            //    }

            //    authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
            //}

            if (!authDbContext.ApiScopes.Any())
            {
                foreach (var apiScope in IdentityConfiguration.GetApiScopes())
                {
                    authDbContext.ApiScopes.Add(apiScope.ToEntity());
                }

                authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
            }

            if (!authDbContext.Clients.Any())
            {
                foreach (var client in IdentityConfiguration.GetClients())
                {
                    authDbContext.Clients.Add(client.ToEntity());
                }

                authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
            }

            if (!authDbContext.Roles.Any())
            {
                foreach (var role in IdentityConfiguration.GetRoles())
                {
                    roleManager.CreateAsync(new AuthRole(role)).GetAwaiter().GetResult();
                }
            }

            if (!authDbContext.Users.Any())
            {
                var user = IdentityConfiguration.GetTestAuthUser();

                userManager.CreateAsync(user, "password")
                    .GetAwaiter().GetResult();

                userManager.AddClaimsAsync(user, new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, Role.User.ToString())
                }).GetAwaiter().GetResult();

                userManager.AddToRoleAsync(user, Role.User.ToString()).GetAwaiter().GetResult();

                var admin = IdentityConfiguration.GetTestAdmin();

                userManager.CreateAsync(admin, "admin")
                    .GetAwaiter().GetResult();

                userManager.AddClaimsAsync(admin, new List<Claim>
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Name, admin.UserName),
                    new Claim(ClaimTypes.Role, Role.Admin.ToString())
                }).GetAwaiter().GetResult();

                userManager.AddToRoleAsync(admin, Role.Admin.ToString()).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, Role.User.ToString());
            }
        }
    }
}
