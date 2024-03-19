using System.Reflection;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Weatherman.Core.Aggregates;
using Weatherman.Core.Interfaces;
using WeatherDDD.SharedKernel;
using IQueueService = Weatherman.Infrastructure.Messaging.IQueueService;

namespace Weatherman.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IQueueService queueService)
        : DbContext(options)
    {
        public DbSet<DailyTemperature> DailyTemperatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var entitiesWithEvents = ChangeTracker
                .Entries()
                .Select(e => e.Entity as BaseEntity<int>)
                .Where(e => e?.Events != null && e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity?.Events.ToArray();
                entity?.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await queueService.SendMessageAsync(domainEvent).ConfigureAwait(false);
                }
            }
            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
