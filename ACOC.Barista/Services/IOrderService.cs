using ACOC.Barista.Models;

namespace ACOC.Barista.Services
{
    public interface IOrderService
    {
       Task<Order> Order(string type);
       Task HandleActiveOrders();
    }
}
