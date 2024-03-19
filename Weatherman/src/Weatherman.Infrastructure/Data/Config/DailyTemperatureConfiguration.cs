using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weatherman.Core.Aggregates;

namespace Weatherman.Infrastructure.Data.Config
{
    public class DailyTemperatureConfiguration : IEntityTypeConfiguration<DailyTemperature>
    {
        public void Configure(EntityTypeBuilder<DailyTemperature> builder)
        {
            builder
              .ToTable("DailyTemperatures").HasKey(k => k.Id);

            builder
                .HasIndex(u => u.Date)
                .IsUnique();
        }
    }
}
