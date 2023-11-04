using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities;

namespace Weather.Infra.Context;

public class DbSqlServerContext : DbContext
{
    public DbSet<CurrentWeather> CurrentWeathers { get; set; }
    public DbSet<DailyWeather> DailyWeathers { get; set; }
    public DbSet<Location> Locations { get; set; }


    public DbSqlServerContext(DbContextOptions<DbSqlServerContext> options) : base(options)
    {            
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        try
        {
            base.OnModelCreating(builder);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }
}