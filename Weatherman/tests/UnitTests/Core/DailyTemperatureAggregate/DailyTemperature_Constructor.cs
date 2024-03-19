using Weatherman.Core.Aggregates;

namespace UnitTests.Core.DailyTemperatureAggregate
{
    public class DailyTemperature_Constructor
    {
        private readonly DateOnly _today = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
        private readonly int _temperature = 15;
        private readonly int _id = 1;


        [Fact]
        public void CreateConstructor()
        {
            // Arrange & Act
            var schedule = new DailyTemperature(_today, _temperature) { Id = _id };

            // Assert
            Assert.Equal(_id, schedule.Id);
            Assert.Equal(_temperature, schedule.TemperatureC);
            Assert.Equal(_today, schedule.Date);
        }
    }
}
