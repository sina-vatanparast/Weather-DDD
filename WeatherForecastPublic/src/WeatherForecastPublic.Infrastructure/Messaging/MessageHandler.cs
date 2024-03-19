using Autofac;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using WeatherForecastPublic.Core.Aggregates;
using WeatherForecastPublic.Core.Interfaces;

namespace WeatherForecastPublic.Infrastructure.Messaging
{
    public class MessageHandler(ILifetimeScope lifetimeScope, IQueueClient queueClient, ILogger<MessageHandler> logger) : IMessageHandler
    {
        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            CreateDailyWeatherMessageModel? messageModel = null;
            try
            {
                messageModel = ParseMessage(message.Body);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An unhandled exception occurred while parsing a poison message with Id :{message.MessageId}");
                await queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
            }

            if (messageModel == null)
            {
                logger.LogError($"Message is empty. MessageID:{message.MessageId}");
                await queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                return;
            }

            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                var repository = scope.Resolve<IDailyWeatherRepository>();
                var dailyWeather = new DailyWeather(messageModel.Date, TranslateToHumanReadable(messageModel.TemperatureC)) { Id = 0 };

                await repository.InsertOrUpdateByDateAsync(dailyWeather);

                await queueClient.CompleteAsync(message.SystemProperties.LockToken);
                logger.LogInformation($"Message with id:{message.MessageId} and CorrelationId: {messageModel.CorrelationId} received and stored successfully");
            }
        }

        private static CreateDailyWeatherMessageModel ParseMessage(byte[] messageBody)
        {
            return JsonConvert.DeserializeObject<CreateDailyWeatherMessageModel>(Encoding.UTF8.GetString(messageBody));
        }

        private static string TranslateToHumanReadable(int temperatureC)
        {
            return temperatureC switch
            {
                < 0 => "Freezing",
                < 10 => "Bracing ",
                < 15 => "Chilly",
                < 20 => "Cool",
                < 25 => "Mild",
                < 28 => "Warm",
                < 33 => "Balmy",
                < 36 => "Hot",
                < 40 => "Sweltering",
                _ => "Scorching"
            };
        }
    }
}
