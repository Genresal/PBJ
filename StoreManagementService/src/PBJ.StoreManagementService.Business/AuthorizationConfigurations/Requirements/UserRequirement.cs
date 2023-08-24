using Microsoft.AspNetCore.Authorization;

namespace PBJ.StoreManagementService.Business.AuthorizationConfigurations.Requirements
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; set; } = null!;

        public List<string> Roles { get; set; } = null!;

        public UserRequirement(string issuer, List<string> roles)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}
