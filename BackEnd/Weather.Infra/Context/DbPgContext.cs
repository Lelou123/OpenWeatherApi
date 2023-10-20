using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities;

namespace Weather.Infra.Context
{
    public class DbPgContext : DbContext
    {

        public DbSet<Domain.Entities.Weather> Weathers { get; set; }
        public DbSet<Location> Locations { get; set; }


        public DbPgContext(DbContextOptions<DbPgContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
}
