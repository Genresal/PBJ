using FluentValidation;
using PBJ.AuthService.Api.RequestModels;

namespace PBJ.AuthService.Api.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserRequestModel>
    {
        public EditUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.Surname).NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.BirthDate).NotNull()
                .WithMessage("Required field");
        }
    }
}
