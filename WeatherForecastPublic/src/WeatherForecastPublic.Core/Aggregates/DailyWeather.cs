using WeatherDDD.SharedKernel;
using WeatherDDD.SharedKernel.Interfaces;

namespace WeatherForecastPublic.Core.Aggregates
{
    public class DailyWeather : BaseEntity<int>, IAggregateRoot
    {
        public DailyWeather(DateOnly date, string weather)
        {
            Date = date;
            Weather = weather;
        }

        public DailyWeather()
        {
                
        }
        public DateOnly Date { get; set; }

        public string Weather { get; set; }
        
    }
}
