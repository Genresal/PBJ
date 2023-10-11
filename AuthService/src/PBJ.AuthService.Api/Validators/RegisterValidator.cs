using FluentValidation;
using PBJ.AuthService.Api.RequestModels;

namespace PBJ.AuthService.Api.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.Surname).NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.BirthDate).NotNull()
                .WithMessage("Required field");

            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Please enter valid email address");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Password do not match");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password required");

            RuleFor(x => x.Password).MinimumLength(4)
                .WithMessage("Password must contain at least 4 characters");

            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .WithMessage("Required field");
        }
    }
}
