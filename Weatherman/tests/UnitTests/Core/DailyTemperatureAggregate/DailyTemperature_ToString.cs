using Weatherman.Core.Aggregates;

namespace UnitTests.Core.DailyTemperatureAggregate
{
    public class DailyTemperature_ToString
    {
        private readonly DateOnly _today = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
        private readonly int _temperatureZero = 0;
        private readonly int _id = 1;

        [Fact]
        public void Correctvalue()
        {
            // Arrange
            var dailyTemperature = new DailyTemperature(_today, _temperatureZero) { Id = _id };

            // Act
            var result = dailyTemperature.ToString();

            // Assert
            Assert.Equal($"{_today}:{_temperatureZero}",result);
        }
    }
}
