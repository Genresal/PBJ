using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PBJ.AuthService.Api.RequestModels;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.Api.Validators
{
    public class EditPasswordValidator : AbstractValidator<EditPasswordRequestModel>
    {
        public EditPasswordValidator(UserManager<AuthUser> userManager)
        {
            RuleFor(x => x.CurrentPassword).NotEmpty()
                .WithMessage("Password required");

            RuleFor(x => x.NewPassword).MinimumLength(4)
                .WithMessage("Password must contain at least 4 characters");

            RuleFor(x => x.NewPassword)
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Password do not match");

            RuleFor(x => x.NewPassword).NotEmpty()
                .WithMessage("Password required");

            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .WithMessage("Required field");
        }
    }
}
