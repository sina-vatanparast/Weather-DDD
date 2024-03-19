using WeatherDDD.SharedKernel;
using WeatherDDD.SharedKernel.Interfaces;
using Weatherman.Core.Events;
using Weatherman.Core.Exceptions;

namespace Weatherman.Core.Aggregates
{
    public class DailyTemperature(DateOnly date, int temperatureC) : BaseEntity<int>, IAggregateRoot
    {
        public DateOnly Date { get; set; } = date;

        public int TemperatureC { get; set; } = temperatureC;

        public void Create(Guid correlationId)
        {
            if (Date < DateOnly.FromDateTime(DateTimeOffset.Now.Date))
            {
                throw new ForecastInPastException(Date);
            }

            if (TemperatureC is < -60 or > 60)
            {
                throw new TemperatureOutOfRangeException(TemperatureC);
            }

            Events.Add(new DailyTemperatureCreatedEvent(Date, TemperatureC, correlationId));
        }

        public override string ToString()
        {
            return $"{Date}:{TemperatureC}";
        }
    }
}
