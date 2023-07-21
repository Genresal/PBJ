using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
        }
    }
}
