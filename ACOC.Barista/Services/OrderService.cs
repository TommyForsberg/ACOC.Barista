using ACOC.Barista.Contracts.http;
using ACOC.Barista.Exceptions;
using ACOC.Barista.Models;
using ACOC.Barista.Repositiories;

namespace ACOC.Barista.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<ProductTemplate> productRepository;
        private readonly IRepository<Order> orderRepository;

        public OrderService(IRepository<ProductTemplate> productRepository, IRepository<Order> orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }
        public async Task<Order> MakeOrder(MakeOrderDTO makeOrderDTO)
        {
            var orderBluePrint = await productRepository.GetAsync(makeOrderDTO.ProductId);
            if (orderBluePrint == null)
                throw new EntityNotFoundException($"No product template found with Id: ${makeOrderDTO.ProductId}");

            Order? order = new(orderBluePrint);
            order.Product.State = makeOrderDTO.ActivateNow ? Models.Enums.LifeCycleState.Activated : Models.Enums.LifeCycleState.Created;
            await orderRepository.CreateAsync(order);

            return order;
        }
        public async Task HandleActiveOrders()
        {
            var orders = await orderRepository.GetByFilter(o => o.Product.State == Models.Enums.LifeCycleState.Activated);
            foreach (var order in orders)
            {
                order.Product.FutureEvents.ToList().ForEach(f =>
                {
                    var now = DateTime.Now;
                    if (f.Date.HasValue && f.Date.Value < now && !f.HasOccured)
                    {
                        f.HasOccured = true;
                        var newEvent = new LifeCycleEvent { HasOccured = true, Description = f.Description, EventType = f.EventType, TimeSpan = f.TimeSpan, Value = f.Value, Title = f.Title, Date = now };
                        order.Product.Events.Add(newEvent);
                        var updated = orderRepository.UpdateAsync(order.Id, order);
                    }
                });
            }
        }
    }
}
