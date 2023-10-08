using PBJ.AuthService.Api.Extensions;
using PBJ.AuthService.Business.Extensions;
using PBJ.AuthService.Business.Services;
using PBJ.AuthService.Business.Services.Abstract;
using PBJ.AuthService.DataAccess.Extensions;

namespace PBJ.AuthService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();

            builder.Services.AddAuthDbContext(builder.Configuration);
            builder.Services.SetupIdentity();
            builder.Services.SetupIdentityServer(builder.Configuration);
            builder.Services.SetupIdentityServerCookie();
            builder.Services.AddJwtBearerAuthentication();
            builder.Services.AddValidations();

            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddSwaggerGen();

            builder.Services.MigrateDatabase();
            builder.Services.InitializeDatabase();

            builder.Services.AddCors();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            app.MapControllers();

            app.Run();
        }
    }
}