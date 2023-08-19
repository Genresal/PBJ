using Microsoft.AspNetCore.Identity;

namespace PBJ.AuthService.DataAccess.Entities
{
    public class AuthRole : IdentityRole<int>
    {
        public AuthRole() : base()
        { }

        public AuthRole(string roleName) : base(roleName)
        { }
    }
}
