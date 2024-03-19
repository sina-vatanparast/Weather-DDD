namespace Weatherman.Api.Models.DailyTemperature
{
    public class CreateDailyTemperatureResponse : BaseResponse
    {
        public CreateDailyTemperatureResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateDailyTemperatureResponse()
        {
        }
    }
}
