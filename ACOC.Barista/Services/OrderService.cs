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
        public async Task<Order> Order(string type)
        {
            var orderBluePrints = await productRepository.GetByFilter(p => p.Name == type);
            if (!orderBluePrints.Any())
                throw new InvalidOperationException();

            Order? order = new Order(orderBluePrints.FirstOrDefault());
            await orderRepository.CreateAsync(order);

            return order;
        }
        public async Task HandleActiveOrders()
        {
            var orders = await orderRepository.GetByFilter(o => o.Product.State == Models.Enums.LifeCycleState.Activated);
        }
    }
}
