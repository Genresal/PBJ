using FluentValidation;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UserValidator : AbstractValidator<UserRequestModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().NotEmpty().NotNull().MaximumLength(50);
        }
    }
}
