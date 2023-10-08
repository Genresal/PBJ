using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using PBJ.AuthService.DataAccess.Entities;
using PBJ.AuthService.DataAccess.Enums;

namespace PBJ.AuthService.Business.Configurations
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("smsAPI", "Access to smsAPI"),
                new ApiScope("testApi", "Access to my external test api"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("smsAPI", "SMS API")
                {
                    Scopes = { "smsAPI" }
                },
                new ApiResource("testApi", "TEST API")
                {
                    Scopes = { "testApi" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "pbj-client",
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://localhost:3000/callback",
                        "http://localhost:3000/refresh"
                    },
                    PostLogoutRedirectUris = { "http://localhost:3000" },
                    AllowedCorsOrigins = { "http://localhost:3000" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "smsAPI",
                    }
                },
                new Client()
                {
                    ClientId = "test-client",
                    ClientName = "Test Client",
                    ClientSecrets = { new Secret("test-secret".Sha256()) },
                    RequireConsent = false,
                    RedirectUris = { "https://localhost:7107/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7107/signout-callback-oidc" },
                    AllowedGrantTypes = { GrantType.AuthorizationCode },
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "testApi",
                    },
                },
                new Client
                {
                    ClientId = "postman-client",
                    ClientName = "Postman Client",
                    ClientSecrets = { new Secret("postman-secret".Sha256()) },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = false,
                    RedirectUris =
                    {
                        "https://www.getpostman.com/oath2/callback",
                        "https://oauth.pstmn.io/v1/callback"
                    },
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "smsAPI",
                    }
                },
                new Client
                {
                    ClientId = "swagger-client",
                    ClientName = "Swagger Client",
                    ClientSecrets = { new Secret("swagger-secret".Sha256()) },
                    RequirePkce = false,
                    AllowedGrantTypes = { GrantType.AuthorizationCode },
                    RedirectUris = { "https://localhost:7231/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins = {"https://localhost:7231"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "smsAPI",
                    }
                },
            };
        }

        public static AuthUser GetTestAuthUser()
        {
            return new AuthUser
            {
                UserName = "Joe",
                Email = "joeDoe@email.com",
                Surname = "Doe",
            };
        }

        public static AuthUser GetTestAdmin()
        {
            return new AuthUser
            {
                UserName = "admin",
                Email = "admin@email.com", 
                Surname = "",
            };
        }

        public static IEnumerable<string> GetRoles()
        {
            return Enum.GetValues(typeof(Role))
                .Cast<Role>()
                .Select(x => x.ToString());
        }
    }
}
