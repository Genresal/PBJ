using FluentValidation;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePostRequestModel>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull().MinimumLength(30);
            RuleFor(x => x.UserEmail).NotNull().NotEmpty().EmailAddress();
        }
    }
}
