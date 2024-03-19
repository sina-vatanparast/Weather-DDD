using Microsoft.EntityFrameworkCore;
using WeatherForecastPublic.Core.Aggregates;
using WeatherForecastPublic.Core.Interfaces;

namespace WeatherForecastPublic.Infrastructure.Data
{
    public class DailyWeatherRepository(AppDbContext dbContext) : BaseRepository<DailyWeather>(dbContext), IDailyWeatherRepository
    {
        public async Task<List<DailyWeather>> GetListWithRangeAsync(DateOnly from, int number)
        {
            return await dbContext.DailyWeathers.Where(p => p.Date >= from && p.Date < from.AddDays(number)).ToListAsync();
        }

        public async Task InsertOrUpdateByDateAsync(DailyWeather model)
        {
            var entity = await dbContext.Set<DailyWeather>().SingleOrDefaultAsync(p => p.Date == model.Date);

            if (entity is null)
            {
                await AddAsync(model);
                return;
            }

            if (entity.Weather == model.Weather)
            {
                // To ignore duplicate inputs
                return;
            }

            entity.Weather = model.Weather;
            await UpdateAsync(entity);
        }
    }
}
