using ACOC.Barista.Models;
using ACOC.Barista.Repositiories;
using ACOC.Barista.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACOC.Barista.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IRepository<Order> orderRepository;

        public OrderController(IOrderService orderService, IRepository<Order> orderRepository)
        {
            this.orderService = orderService;
            this.orderRepository = orderRepository;
        }
        [HttpGet(Name = "Order"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        public async  Task<IActionResult> Get([FromQuery] string type)
        {
            Order order = await orderService.Order(type);
            return  Ok(order);
        }

        [HttpGet("{friendlyUID}/peek"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        public async Task<IActionResult> Peek(string friendlyUID)
        {
            var order = await orderRepository.GetFirstOrDefaultByFilter(r => r.FriendlyUUID == friendlyUID);
            return Ok(order);
        }
        //[HttpGet(Name = "Order/{friendlyUID}/consume"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        //public IActionResult Consume(string friendlyUID)
        //{
        //    return Ok();
        //}
    }
}
