using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using WeatherDDD.SharedKernel;

namespace Weatherman.Infrastructure.Messaging
{
    public class QueueService(ISenderClient serviceBusSenderClient) : IQueueService
    {
        public async Task SendMessageAsync(BaseDomainEvent domainEvent)
        {
            var jsonString = JsonConvert.SerializeObject(domainEvent);
            var message = new Message(Encoding.UTF8.GetBytes(jsonString))
            {
                MessageId = Guid.NewGuid().ToString()
            };
            await serviceBusSenderClient.SendAsync(message).ConfigureAwait(false);
        }

        public async Task CloseAsync()
        {
            await serviceBusSenderClient.CloseAsync();
        }
    }
}
