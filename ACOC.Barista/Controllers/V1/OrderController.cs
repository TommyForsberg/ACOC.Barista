using ACOC.Barista.Contracts.http;
using ACOC.Barista.Exceptions;
using ACOC.Barista.Models;
using ACOC.Barista.Repositiories;
using ACOC.Barista.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACOC.Barista.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IOrderService orderService;
        private readonly IRepository<Order> orderRepository;

        public OrderController(ILogger<ProductController> logger, IOrderService orderService, IRepository<Order> orderRepository)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.orderRepository = orderRepository;
        }
        [HttpPost(Name = "Order"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        public async  Task<IActionResult> Post(MakeOrderDTO makeOrderDTO)
        {
            try
            {
                Order order = await orderService.MakeOrder(makeOrderDTO);
                return Ok(order);
            }
            catch(EntityNotFoundException ex)
            {
                logger.LogWarning(exception: ex, "Could not make order");
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error when posting product template");
                throw;        
            }
           
        }

        [HttpGet("/{id}/peek"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        public async Task<IActionResult> Peek(string id)
        {
            var order = await orderRepository.GetAsync(id);
            return Ok(order);
        }
        [HttpGet(Name = "{friendlyUUID}/consume"), ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        public async Task<IActionResult> Consume(string friendlyUUID)
        {
            var order = await orderRepository.GetFirstOrDefaultByFilter(o => o.FriendlyUUID == friendlyUUID);
            order.Product.State = Models.Enums.LifeCycleState.Consumed;
            await orderRepository.UpdateAsync(order.Id, order);
            return Ok();
        }
    }
}
