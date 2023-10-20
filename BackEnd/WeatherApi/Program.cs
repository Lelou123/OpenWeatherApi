
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Weather.Application.Services;
using Weather.Domain.Interfaces.Repositories;
using Weather.Domain.Interfaces.Services;
using Weather.Infra.AutoMapper;
using Weather.Infra.Context;
using Weather.Infra.Repositories;

namespace WeatherApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddDbContext<DbPgContext>(opt => opt.UseLazyLoadingProxies()
                .UseNpgsql(builder.Configuration.GetConnectionString("ConnectionPG")));


            
            
            //ApplicationServices
            builder.Services.AddTransient<IWeatherService, WeatherService>();



            //Others Services
            builder.Services.AddTransient<IAutoMapperService, AutoMapperService>();


            //Repositories
            builder.Services.AddTransient<ICurrentWeatherRepository, CurrentWeatherRepository>();
            builder.Services.AddTransient<IDailyWeatherRepository, DailyWeatherRepository>();
            builder.Services.AddTransient<ILocationRepository, LocationRepository>();



            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .WithExposedHeaders("Content-Disposition"));
            });


            //Configure Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherApi", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);                
            });

            var app = builder.Build();

            app.UseCors("CorsPolicy");

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