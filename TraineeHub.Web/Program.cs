using Microsoft.EntityFrameworkCore;
using TraineeHub.Application.Interfaces;
using TraineeHub.Messaging;
using TraineeHub.Messaging.Common.Interfaces;
using TraineeHub.Infrastructure.Email;
using TraineeHub.Infrastructure.Messaging;
using TraineeHub.Infrastructure.Persistence;
using TraineeHub.Infrastructure.Services;

namespace TraineeHub.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TraineeHubDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "TraineeHub_";
            });
            builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
            builder.Services.AddHostedService<RabbitMqConsumer>();
            builder.Services.AddSingleton<IEmailService, EmailService>();
         
            builder.Services.AddHostedService<RabbitMqConsumer>();
            builder.Services.AddScoped<IExportService, ExportService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Trainees}/{action=index}/{id?}");

            app.Run();
        }
    }
}