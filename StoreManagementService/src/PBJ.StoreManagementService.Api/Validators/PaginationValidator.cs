using FluentValidation;
using PBJ.StoreManagementService.Models.Pagination;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class PaginationValidator : AbstractValidator<PaginationRequestModel>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Take).GreaterThan(0).NotEmpty();
        }
    }
}
