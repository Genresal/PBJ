﻿using FluentValidation;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Api.Validators
{
    public class PostValidator : AbstractValidator<PostRequestModel>
    {
        public PostValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull().MinimumLength(30);
            RuleFor(x => x.UserId).Must(x => x > 0);
        }
    }
}
