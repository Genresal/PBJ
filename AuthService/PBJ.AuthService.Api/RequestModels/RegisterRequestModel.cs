﻿namespace PBJ.AuthService.Api.RequestModels
{
    public class RegisterRequestModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
