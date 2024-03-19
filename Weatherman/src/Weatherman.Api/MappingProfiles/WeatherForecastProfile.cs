using AutoMapper;
using Weatherman.Api.Models.DailyTemperature;
using Weatherman.Core.Aggregates;

namespace Weatherman.Api.MappingProfiles
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<CreateDailyTemperatureRequest, DailyTemperature>();
        }
    }
}
