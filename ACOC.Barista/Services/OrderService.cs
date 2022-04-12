using ACOC.Barista.Contracts.http;
using ACOC.Barista.Exceptions;
using ACOC.Barista.Models;
using ACOC.Barista.Repositiories;
using Azure.Messaging.ServiceBus;

namespace ACOC.Barista.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<ProductTemplate> productRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IServiceBusClientProvider serviceBusClientProvider;

        public OrderService(IRepository<ProductTemplate> productRepository, IRepository<Order> orderRepository, IServiceBusClientProvider serviceBusClientProvider)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.serviceBusClientProvider = serviceBusClientProvider;
        }
        public async Task<Order> MakeOrder(MakeOrderDTO makeOrderDTO)
        {
            var orderBluePrint = await productRepository.GetAsync(makeOrderDTO.ProductId);
            if (orderBluePrint == null)
                throw new EntityNotFoundException($"No product template found with Id: ${makeOrderDTO.ProductId}");

            Order? order = new(orderBluePrint);
            order.Product.State = makeOrderDTO.ActivateNow ? Models.Enums.LifeCycleState.Activated : Models.Enums.LifeCycleState.Created;
            order.Webhook = makeOrderDTO.Webhook;
            await orderRepository.CreateAsync(order);

            return order;
        }
        public async Task HandleActiveOrders()
        {
            var orders = await orderRepository.GetByFilter(o => o.Product.State == Models.Enums.LifeCycleState.Activated);
            var now = DateTime.UtcNow;
            foreach (var order in orders)
            {
                //Dispose product if older then 2 hours and state Activated
                order.Product.State = order.OrderDateTime.AddHours(2) < order.OrderDateTime && order.Product.State == Models.Enums.LifeCycleState.Activated ? Models.Enums.LifeCycleState.Disposed : Models.Enums.LifeCycleState.Activated;

                order.Product.FutureEvents.ToList().ForEach(async f =>
                {
                   
                    if (f.Date.HasValue && f.Date.Value < now && !f.HasOccured)
                    {
                        f.HasOccured = true;
                        var newEvent = new LifeCycleEvent { HasOccured = true, Description = f.Description, EventType = f.EventType, TimeSpan = f.TimeSpan, Value = f.Value, Title = f.Title, Date = now };
                        order.Product.Events.Add(newEvent);
                        await serviceBusClientProvider.Send("callback", new Contracts.servicebus.MessageDTO { Callback = order.Webhook, Body = newEvent });
                    }
                    
                });
                var updated = orderRepository.UpdateAsync(order.Id, order);
            }
        }
    }
}
