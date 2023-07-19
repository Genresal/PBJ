using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PBJ.StoreManagementService.Business.Services;
using PBJ.StoreManagementService.Business.Services.Abstract;

namespace PBJ.StoreManagementService.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserFollowersService, UserFollowersService>();
        }
    }
}
