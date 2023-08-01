using FluentValidation;
using PBJ.StoreManagementService.Models.Comment;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentRequestModel>

    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull();
        }
    }
}
