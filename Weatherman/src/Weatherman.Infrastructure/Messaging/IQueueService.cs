using WeatherDDD.SharedKernel;

namespace Weatherman.Infrastructure.Messaging
{
    public interface IQueueService
    {
        Task SendMessageAsync(BaseDomainEvent domainEvent);
        Task CloseAsync();
    }
}
