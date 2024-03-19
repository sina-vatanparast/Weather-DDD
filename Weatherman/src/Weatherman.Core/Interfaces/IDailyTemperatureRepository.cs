using WeatherDDD.SharedKernel.Interfaces;
using Weatherman.Core.Aggregates;

namespace Weatherman.Core.Interfaces
{
    public interface IDailyTemperatureRepository : IRepository<DailyTemperature>
    {
        Task InsertOrUpdateByDateAsync(DailyTemperature model);
    }
}
