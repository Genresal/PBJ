using PBJ.AuthService.Api.Extensions;
using PBJ.AuthService.Business.Extensions;
using PBJ.AuthService.DataAccess.Extensions;

namespace PBJ.AuthService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            
            builder.Services.AddAuthDbContext(builder.Configuration);
            builder.Services.SetupIdentity();
            builder.Services.SetupIdentityServer(builder.Configuration);
            builder.Services.InititalizeDatabase();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
          
            app.UseAuthorization();

            app.UseIdentityServer();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}