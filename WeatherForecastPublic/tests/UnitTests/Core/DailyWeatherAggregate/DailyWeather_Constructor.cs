using WeatherForecastPublic.Core.Aggregates;

namespace UnitTests.Core.DailyWeatherAggregate
{
    public class DailyWeather_Constructor
    {
        private readonly DateOnly _today = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
        private readonly string _weather = "Freezing";
        private readonly int _id = 1;


        [Fact]
        public void CreateConstructor()
        {
            // Arrange & Act
            var schedule = new DailyWeather(_today, _weather) { Id = _id };

            // Assert
            Assert.Equal(_id, schedule.Id);
            Assert.Equal(_weather, schedule.Weather);
            Assert.Equal(_today, schedule.Date);
        }
    }
}
