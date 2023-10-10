using FluentValidation;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePostRequestModel>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.UserEmail).NotNull().NotEmpty().EmailAddress();
        }
    }
}
