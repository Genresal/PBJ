using FluentValidation;
using PBJ.StoreManagementService.Api.Validators;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Post;
using PBJ.StoreManagementService.Models.User;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddNewtonsoftJson(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserRequestModel>, UserValidator>();
            services.AddScoped<IValidator<PostRequestModel>, PostValidator>();
            services.AddScoped<IValidator<CommentRequestModel>, CommentValidator>();
            services.AddScoped<IValidator<UserFollowersRequestModel>, UserFollowerValidator>();
        }
    }
}
