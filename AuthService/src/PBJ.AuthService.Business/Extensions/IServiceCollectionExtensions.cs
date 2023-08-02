using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PBJ.AuthService.Business.Configurations;
using PBJ.AuthService.DataAccess.Context;
using PBJ.AuthService.DataAccess.Entities;

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

            if (!authDbContext.ApiResources.Any())
            {
                foreach (var apiResource in IdentityConfiguration.GetApiResources())
                {
                    authDbContext.ApiResources.Add(apiResource.ToEntity());
                }

                authDbContext.SaveChangesAsync().GetAwaiter().GetResult();
            }

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

            if (!authDbContext.Users.Any())
            {
                userManager.CreateAsync(IdentityConfiguration.GetTestAuthUser(), "password")
                    .GetAwaiter().GetResult();
            }

            if (authDbContext.Roles.Any())
            {
                foreach (var role in IdentityConfiguration.GetRoles())
                {
                    roleManager.CreateAsync(new AuthRole(role)).GetAwaiter().GetResult();
                }
            }
        }
    }
}
