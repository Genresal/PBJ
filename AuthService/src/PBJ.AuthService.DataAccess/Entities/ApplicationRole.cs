using Microsoft.AspNetCore.Identity;

namespace PBJ.AuthService.DataAccess.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base()
        { }

        public ApplicationRole(string roleName) : base(roleName)
        { }
    }
}
