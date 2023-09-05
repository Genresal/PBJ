using RazorLight.Extensions;
using System.Reflection;

namespace PBJ.NotificationService.Presentation.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void SetupRazorLight(this IServiceCollection services)
        {
            services.AddRazorLight()
                .UseEmbeddedResourcesProject(Assembly.GetExecutingAssembly().GetType())
                .UseMemoryCachingProvider()
                .SetOperatingAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
