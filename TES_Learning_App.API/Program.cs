using Microsoft.EntityFrameworkCore;
using TES_Learning_App.Infrastructure.Data;
using TES_Learning_App.Application_Layer.Interfaces;
using TES_Learning_App.Application_Layer.Services;
using TES_Learning_App.Domain.Interfaces.Repositories;
using TES_Learning_App.Infrastructure.Repositories;
using AutoMapper;
using TES_Learning_App.API.Profiles;

namespace TES_Learning_App.API

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // NEW DATABASE WIRING CODE
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();

            // Register AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register Services
            builder.Services.AddScoped<ILevelService, LevelService>();
            
            // Register Repositories
            builder.Services.AddScoped<ILevelRepository, LevelRepository>();
          
            // 2. Build the application.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
