using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants.Enums;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Handlers
{
    public static class JwtTokenHandler
    {
        private static string _secretKey = "Super secret secretTestKey";

        public static string UserToken => 
            GenerateToken(TestingConstants.UserUsername, TestingConstants.UserEmail, Role.User);

        public static string AdminToken =>
            GenerateToken(TestingConstants.AdminUsername, TestingConstants.AdminEmail, Role.Admin);

        private static string GenerateToken(string username, string email, Role role)
        {
            var claims = new List<Claim>()
            {
                new (ClaimTypes.Name, username),
                new (ClaimTypes.Email, email),
                new (ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                                        claims: claims, 
                                        expires: DateTime.Now.AddHours(1), 
                                        signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
