
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Weather.Application.Services;
using Weather.Domain.Interfaces;
using Weather.Domain.Interfaces.Repositories;
using Weather.Domain.Interfaces.Services;
using Weather.Domain.Interfaces.Services.ExternalServices;
using Weather.Infra.AutoMapper;
using Weather.Infra.Context;
using Weather.Infra.Repositories;
using Weather.Infra.Services;
using Weather.Infra.Validators;

namespace WeatherApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddDbContext<DbPgContext>(opt => opt.UseLazyLoadingProxies()
            //    .UseNpgsql(builder.Configuration.GetConnectionString("ConnectionPG")));


            builder.Services.AddDbContext<DbSqlServerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionPG")));




            BuildServices(builder);

            BuildRepositories(builder);


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


            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }


        private static void BuildServices(WebApplicationBuilder builder)
        {

            //ApplicationServices
            builder.Services.AddScoped<IWeatherService, WeatherService>();


            //Others Services
            builder.Services.AddScoped<IMappingService, AutoMapperService>();
            builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();

            //Validators
            builder.Services.AddScoped<IWeatherGetDtoValidator, WeatherGetDtoValidator>();

        }

        private static void BuildRepositories(WebApplicationBuilder builder)
        {

            //Repositories
            builder.Services.AddScoped<ICurrentWeatherRepository, CurrentWeatherRepository>();
            builder.Services.AddScoped<IDailyWeatherRepository, DailyWeatherRepository>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
        }
    }
}