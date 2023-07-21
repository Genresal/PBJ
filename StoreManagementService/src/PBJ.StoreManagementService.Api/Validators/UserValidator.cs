using FluentValidation;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UserValidator : AbstractValidator<UserRequestModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(x => x.Lastname).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(x => x.Surname).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(x => x.Email)
                .EmailAddress().NotEmpty().NotNull().MaximumLength(50);
        }
    }
}
