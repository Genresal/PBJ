﻿using IdentityServer4;
using IdentityServer4.Models;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Business.Configurations
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),

            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("smsAPI", "Access to smsAPI"),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("smsAPI", "SMS API")
                {
                    Scopes = { "smsAPI" }
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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "smsAPI",
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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
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
                UserName = "Joe Doe",
                Email = "joeDoe@email.com"
            };
        }

        public static IEnumerable<string> GetRoles()
        {
            return Enum.GetValues(typeof(AuthRole))
                .Cast<AuthRole>()
                .Select(x => x.ToString());
        }
    }
}
