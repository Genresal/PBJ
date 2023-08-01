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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MigrateDatabases();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}