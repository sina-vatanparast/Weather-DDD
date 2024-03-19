using WeatherForecastPublic.Core.Aggregates;
using WeatherDDD.SharedKernel.Interfaces;

namespace WeatherForecastPublic.Core.Interfaces
{
    public interface IDailyWeatherRepository : IRepository<DailyWeather>
    {
        public Task<List<DailyWeather>> GetListWithRangeAsync(DateOnly from, int number);

        Task InsertOrUpdateByDateAsync(DailyWeather model);
    }
}
