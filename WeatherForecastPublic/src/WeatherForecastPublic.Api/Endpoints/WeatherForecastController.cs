using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeatherForecastPublic.Api.Models.WeatherForecast;
using WeatherForecastPublic.Core.Interfaces;

namespace WeatherForecastPublic.Api.Endpoints
{
    [ApiController]
    [Route("api/WeatherForecast")]
    public class WeatherForecastController(IDailyWeatherRepository repository)
        : ControllerBase
    {
        private const int WeekDayNumbers = 7;
        private const string Today = "today";
        private const string Unspecified = "unspecified";

        [HttpGet("Week")]
        [SwaggerOperation(
            Summary = "Gets weekly weather forecast",
            Description = "Returns weather forecast for a week from today",
            OperationId = "weatherForecast.week",
            Tags = ["weatherForecastEndpoints"])
        ]
        public async Task<WeeklyWeatherForecastResponse> WeekAsync()
        {
            var today = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
            var existingDayWeathers = await repository.GetListWithRangeAsync(today, WeekDayNumbers);
            var response = new WeeklyWeatherForecastResponse();

            for (var i = 0; i < WeekDayNumbers; i++)
            {
                var day = today.AddDays(i);
                var dayWeather = existingDayWeathers.FirstOrDefault(p => p.Date == day);
                var dayName = i == 0 ? Today : $"{day.DayOfWeek}({day})";
                response.Days.Add(dayName, dayWeather == null ? Unspecified : dayWeather.Weather);
            }

            return response;
        }
    }
}
