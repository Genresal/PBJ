using FluentValidation;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UserFollowerValidator : AbstractValidator<UserFollowersRequestModel>
    {
        public UserFollowerValidator()
        {
            RuleFor(x => x.UserId).Must(x => x > 0);
            RuleFor(x => x.FollowerId).Must(x => x > 0);
        }
    }
}
