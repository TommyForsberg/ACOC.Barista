using ACOC.Barista.Contracts.http;
using ACOC.Barista.Models;

namespace ACOC.Barista.Services
{
    public interface IOrderService
    {
       Task<Order> MakeOrder(MakeOrderDTO makeOrderDTO);
       Task HandleActiveOrders();
    }
}
