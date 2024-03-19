namespace WeatherForecastPublic.Infrastructure.Messaging
{
    public class CreateDailyWeatherMessageModel
    {
        public Guid CorrelationId { get; set; }
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
    }
}
