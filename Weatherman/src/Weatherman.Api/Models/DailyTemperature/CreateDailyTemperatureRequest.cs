using System.ComponentModel.DataAnnotations;

namespace Weatherman.Api.Models.DailyTemperature
{
    public class CreateDailyTemperatureRequest : BaseRequest
    {
        [Required(ErrorMessage = "Date is required.")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "TemperatureC is required.")]
        public int TemperatureC { get; set; }
    }
}
