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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthDbContext(builder.Configuration);
            builder.Services.SetupIdentity();
            builder.Services.SetupIdentityServer(builder.Configuration);
            builder.Services.InititalizeDatabase();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseIdentityServer();

            app.MapControllers();

            app.Run();
        }
    }
}