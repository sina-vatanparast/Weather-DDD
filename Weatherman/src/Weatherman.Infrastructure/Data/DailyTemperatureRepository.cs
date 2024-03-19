using Microsoft.EntityFrameworkCore;
using Weatherman.Core.Aggregates;
using Weatherman.Core.Interfaces;

namespace Weatherman.Infrastructure.Data
{
    public class DailyTemperatureRepository(AppDbContext dbContext) : BaseRepository<DailyTemperature>(dbContext), IDailyTemperatureRepository
    {
        public async Task InsertOrUpdateByDateAsync(DailyTemperature model)
        {
            var entity = await dbContext.Set<DailyTemperature>().SingleOrDefaultAsync(p => p.Date == model.Date);

            if (entity is null)
            {
                await AddAsync(model);
                return;
            }

            if (entity.TemperatureC == model.TemperatureC)
            {
                // To ignore duplicate inputs
                return;
            }

            entity.TemperatureC = model.TemperatureC;

            if(model.Events != null && model.Events.Any())
            {
                entity.Events.Add(model.Events.First());
            }
            
            await UpdateAsync(entity);
        }
    }
}
