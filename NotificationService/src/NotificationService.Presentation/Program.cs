using PBJ.NotificationService.Application.Extensions;
using PBJ.NotificationService.Domain.Extensions;
using PBJ.NotificationService.Presentation.Extensions;
namespace PBJ.NotificationService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDomainOptions(builder.Configuration);
            builder.Services.AddApplicationDependencies();
            builder.Services.SetupMassTransit();
            builder.Services.SetupRazorLight();
            
            var app = builder.Build();

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