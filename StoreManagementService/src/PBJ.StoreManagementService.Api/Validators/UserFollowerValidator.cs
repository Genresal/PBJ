using FluentValidation;
using PBJ.StoreManagementService.Api.RequestModels;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UserFollowerValidator : AbstractValidator<UserFollowerRequestModel>
    {
        public UserFollowerValidator()
        {
            RuleFor(x => x.UserId).Must(x => x > 0);
            RuleFor(x => x.FollowerId).Must(x => x > 0);
        }
    }
}
