using FluentValidation;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UpdatePostValidator : AbstractValidator<UpdatePostRequestModel>
    {
        public UpdatePostValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull().MinimumLength(30);
        }
    }
}
