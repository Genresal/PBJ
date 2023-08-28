using Microsoft.AspNetCore.Authorization;

namespace PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; set; } = null!;

        public string Role { get; set; } = null!;

        public UserRequirement(string issuer, string role)
        {
            Issuer = issuer ?? throw new ArgumentNullException(); 
            Role = role ?? throw new ArgumentNullException();
        }
    }
}
