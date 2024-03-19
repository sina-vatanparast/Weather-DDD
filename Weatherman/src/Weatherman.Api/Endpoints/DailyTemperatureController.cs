using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Weatherman.Api.Models.DailyTemperature;
using Weatherman.Core.Aggregates;
using Weatherman.Core.Interfaces;

namespace Weatherman.Api.Endpoints
{
    [ApiController]
    [Route("api/DailyTemperature")]
    public class DailyTemperatureController(IDailyTemperatureRepository repository, IMapper mapper, ILogger<DailyTemperatureController> logger)
        : ControllerBase
    {
        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Creates a new temperature for a specific day",
            Description = "Creates a new weather forecast for a day in the future",
            OperationId = "dailyTemperature.create",
            Tags = ["weathermanEndpoints"])
        ]
        public async Task<CreateDailyTemperatureResponse> CreateAsync(CreateDailyTemperatureRequest request)
        {
            var response = new CreateDailyTemperatureResponse(request.CorrelationId);

            var toAdd = mapper.Map<DailyTemperature>(request);
            toAdd.Create(request.CorrelationId);
            await repository.InsertOrUpdateByDateAsync(toAdd);

            logger.LogInformation($"Sending daily temperature: {toAdd} - CorrelationId:{request.CorrelationId}");

            return response;
        }
    }
}
