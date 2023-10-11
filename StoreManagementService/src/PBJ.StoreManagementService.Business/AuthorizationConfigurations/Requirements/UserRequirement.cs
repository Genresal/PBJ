using Microsoft.AspNetCore.Authorization;

namespace PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public UserRequirement(string issuer, string role)
        {
            Issuer = issuer ?? throw new ArgumentNullException();
            Role = role ?? throw new ArgumentNullException();
        }

        public string Issuer { get; set; }

        public string Role { get; set; }
    }
}
