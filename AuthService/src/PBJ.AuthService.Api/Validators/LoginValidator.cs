using FluentValidation;
using PBJ.AuthService.Api.RequestModels;

namespace PBJ.AuthService.Api.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Please enter valid email address");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email required");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password required");

            RuleFor(x => x.Password).MinimumLength(4)
                .WithMessage("Password must contain at least 4 characters");
        }
    }
}
