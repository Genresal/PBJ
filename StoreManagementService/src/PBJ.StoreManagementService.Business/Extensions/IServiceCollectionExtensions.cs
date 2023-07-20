using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PBJ.StoreManagementService.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
