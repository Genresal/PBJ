using FluentValidation;
using PBJ.StoreManagementService.Models.Comment;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class CommentValidator : AbstractValidator<CommentRequestModel>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.UserId).Must(x => x > 0);
            RuleFor(x => x.PostId).Must(x => x > 0);
        }
    }
}
