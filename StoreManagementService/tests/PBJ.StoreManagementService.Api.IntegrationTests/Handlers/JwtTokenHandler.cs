﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants;
using PBJ.StoreManagementService.Api.IntegrationTests.Constants.Enums;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Handlers
{
    public static class JwtTokenHandler
    {
        private static readonly string _secretKey = "Super secret secretTestKey";

        public static string UserToken =>
            GenerateToken(AuthConstants.UserUsername, AuthConstants.UserEmail, Role.User);

        public static string AdminToken =>
            GenerateToken(AuthConstants.AdminUsername, AuthConstants.AdminEmail, Role.Admin);

        private static string GenerateToken(string username, string email, Role role)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                AuthConstants.Issuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
