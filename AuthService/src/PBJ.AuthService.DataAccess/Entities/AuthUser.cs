using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PBJ.AuthService.DataAccess.Entities
{
    public class AuthUser : IdentityUser<int>
    {
        [Required] 
        public string Surname { get; set; } = null!;

        [Required] 
        public DateTime BirthDate { get; set; }
    }
}
