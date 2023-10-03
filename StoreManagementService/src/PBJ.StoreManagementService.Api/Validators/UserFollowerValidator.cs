using FluentValidation;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UserFollowerValidator : AbstractValidator<UserFollowersRequestModel>
    {
        public UserFollowerValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotNull().NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(x => x.FollowerEmail)
                .NotNull().NotEmpty().EmailAddress().MaximumLength(50);
        }
    }
}
