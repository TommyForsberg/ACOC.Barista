using ACOC.Barista.Contracts.servicebus;
using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace ACOC.Barista.Services
{
    public class ServiceBusClientProvider : IServiceBusClientProvider
    {
        private readonly Dictionary<string, ServiceBusSender> senders;
        private readonly ILogger<ServiceBusClientProvider> logger;

        public ServiceBusClientProvider(ServiceBusClient serviceBusClient, ILogger<ServiceBusClientProvider> logger)
        {
            senders = new Dictionary<string, ServiceBusSender> { { "callback", serviceBusClient.CreateSender("callback") } };
            this.logger = logger;
        }
        public async Task Send(string queueName, MessageDTO message)
        {
            try
            {
                var sender = senders[queueName];
                var json = JsonSerializer.Serialize(message);
                await sender.SendMessageAsync(new ServiceBusMessage(json));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not send message on queue");
            }      
        }            
    }
}
