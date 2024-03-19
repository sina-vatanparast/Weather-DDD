using Weatherman.Core.Aggregates;
using Weatherman.Core.Events;
using Weatherman.Core.Exceptions;

namespace UnitTests.Core.DailyTemperatureAggregate
{
    public class DailyTemperature_Create
    {
        private readonly DateOnly _today = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
        private readonly int _temperatureZero = 0;
        private readonly int _id = 1;
        private readonly Guid _correlationId = Guid.NewGuid();

        [Theory]
        [InlineData(1,-60)]
        [InlineData(2,0)]
        [InlineData(100,60)]
        public void CreateSuccess(int daystoAdd, int temperature)
        {
            // Arrange
            var dailyTemperature = new DailyTemperature(_today.AddDays(daystoAdd), temperature) { Id = _id };

            // Act
            dailyTemperature.Create(_correlationId);

            // Assert
            Assert.Single(dailyTemperature.Events);
            Assert.Contains(dailyTemperature.Events, x => x.GetType() == typeof(DailyTemperatureCreatedEvent));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ThrowsForecastInPastExceptionGivenPastDate(int daystoDeduct)
        {
            // Arrange
            var dailyTemperature = new DailyTemperature(_today.AddDays(daystoDeduct), _temperatureZero) { Id = _id };

            // Act
            void Action() => dailyTemperature.Create(_correlationId);

            // Assert
            Assert.Throws<ForecastInPastException>(Action);
        }

        [Theory]
        [InlineData(-61)]
        [InlineData(61)]
        [InlineData(100)]
        public void ThrowsTemperatureOutOfRangeExceptionGivenOutOfRangeTemperature(int temperature)
        {
            // Arrange
            var dailyTemperature = new DailyTemperature(_today, temperature) { Id = _id };

            // Act
            void Action() => dailyTemperature.Create(_correlationId);

            // Assert
            Assert.Throws<TemperatureOutOfRangeException>(Action);
        }
    }
}
