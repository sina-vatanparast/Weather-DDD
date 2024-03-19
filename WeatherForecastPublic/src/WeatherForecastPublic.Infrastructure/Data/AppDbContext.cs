using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WeatherForecastPublic.Core.Aggregates;

namespace WeatherForecastPublic.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<DailyWeather> DailyWeathers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
