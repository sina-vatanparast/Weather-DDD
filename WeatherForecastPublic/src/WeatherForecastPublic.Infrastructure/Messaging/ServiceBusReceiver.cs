using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace WeatherForecastPublic.Infrastructure.Messaging
{
    public class ServiceBusReceiver(IMessageHandler messageHandler, IQueueClient queueClient, ILogger<ServiceBusReceiver> logger)
    {
        public void RegisterMessageHandler()
        {
            queueClient.RegisterMessageHandler(
                messageHandler.HandleMessageAsync,
                new MessageHandlerOptions(ExceptionReceivedHandler) { MaxConcurrentCalls = 1, AutoComplete = false });
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            // Handle exceptions
            logger.LogError(exceptionReceivedEventArgs.Exception, "An unhandled exception occurred while receiving a message");
            return Task.CompletedTask;
        }
    }
}
