using ACOC.Barista.Models;
using ACOC.Barista.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACOC.Barista.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet(Name = "Order"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        public async  Task<IActionResult> Get([FromQuery] string type)
        {
            Order order = await orderService.Order(type);
            return  Ok(order);
        }

        //[HttpGet(Name = "Order/{friendlyUID}/peek"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        //public IActionResult Peek(string friendlyUID)
        //{
        //    return Ok();
        //}
        //[HttpGet(Name = "Order/{friendlyUID}/consume"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        //public IActionResult Consume(string friendlyUID)
        //{
        //    return Ok();
        //}
    }
}
