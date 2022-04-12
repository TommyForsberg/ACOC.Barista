using ACOC.Barista.Contracts.servicebus;

namespace ACOC.Barista.Services
{
    public interface IServiceBusClientProvider
    {
        Task Send(string queueName, MessageDTO message);
    }
}
