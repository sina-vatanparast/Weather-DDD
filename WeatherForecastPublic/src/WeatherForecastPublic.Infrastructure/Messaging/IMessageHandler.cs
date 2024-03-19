using Microsoft.Azure.ServiceBus;

namespace WeatherForecastPublic.Infrastructure.Messaging
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(Message message, CancellationToken cancellationToken);
    }
}
