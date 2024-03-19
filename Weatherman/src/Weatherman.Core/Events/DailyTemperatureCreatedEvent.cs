using WeatherDDD.SharedKernel;

namespace Weatherman.Core.Events
{
    public class DailyTemperatureCreatedEvent(DateOnly date, int temperatureC, Guid correlationId) : BaseDomainEvent
    {
        public Guid CorrelationId { get; set; } = correlationId;

        public DateOnly Date { get; set; } = date;

        public int TemperatureC { get; set; } = temperatureC;
    }
}
