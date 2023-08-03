using IdentityServer4;
using IdentityServer4.Models;
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
                new ApiScope("testApi", "Access to my external test api")
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
                    ClientId = "react-client",
                    ClientName = "React Client",
                    ClientSecrets = { new Secret("react-secret".Sha256()) },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = false,
                    RedirectUris = { "http://localhost:<REACT-PORT>/callback", "http://localhost:<REACT-PORT>/refresh" },
                    PostLogoutRedirectUris = { "http://localhost:<REACT-PORT>/logout" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "smsAPI",
                    }
                },
                new Client()
                {
                    ClientId = "test-client",
                    ClientName = "Test Client",
                    ClientSecrets = { new Secret("test-secret".Sha256()) },
                    RequireConsent = false,
                    RedirectUris = { "https://localhost:7107/signin-oidc", "https://localhost:7107/Auth/Login" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "testApi",
                    }
                },
                new Client
                {
                    ClientId = "postman-client",
                    ClientName = "Postman Client",
                    ClientSecrets = { new Secret("postman-secret".Sha256()) },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = false,
                    RedirectUris = { "https://www.getpostman.com/oath2/callback" },
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
                        "smsAPI",
                    }
                }
            };
        }

        public static AuthUser GetTestAuthUser()
        {
            return new AuthUser
            {
                UserName = "JoeDoe",
                Email = "joeDoe@email.com"
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
