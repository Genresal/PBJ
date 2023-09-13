using PBJ.StoreManagementService.Api.Extensions;
using PBJ.StoreManagementService.Api.Middlewares;
using PBJ.StoreManagementService.Business.Extensions;
using PBJ.StoreManagementService.DataAccess.Extensions;

namespace PBJ.StoreManagementService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDatabaseContext(builder.Configuration);
            builder.Services.AddDataAccessDependencies();
            builder.Services.AddServices();
            builder.Services.AddAutoMapper();
            builder.Services.AddNewtonsoftJson();
            builder.Services.AddValidators();
            builder.Services.BuildSerilog();

            builder.Services.BuildOptions(builder.Configuration);
            builder.Services.SetupAuthentication();
            builder.Services.SetupAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            var app = builder.Build();
            
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SMS V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.MapControllers();

            app.Run();
        }
    }
}