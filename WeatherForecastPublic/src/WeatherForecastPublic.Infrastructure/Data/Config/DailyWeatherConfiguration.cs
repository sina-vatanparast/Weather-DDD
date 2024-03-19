using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherForecastPublic.Core.Aggregates;

namespace WeatherForecastPublic.Infrastructure.Data.Config
{
    public class DailyWeatherConfiguration : IEntityTypeConfiguration<DailyWeather>
    {
        public void Configure(EntityTypeBuilder<DailyWeather> builder)
        {
            builder
              .ToTable("DailyWeathers").HasKey(k => k.Id);

            builder
                .HasIndex(u => u.Date)
                .IsUnique();

            builder.Property(p => p.Weather).HasMaxLength(15);
        }
    }
}
