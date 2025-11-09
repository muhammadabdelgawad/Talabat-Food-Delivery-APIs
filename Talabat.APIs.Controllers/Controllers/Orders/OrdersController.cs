using Talabat.Application.Abstraction.Models.Orders;

namespace Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) :BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto)
        {
            var buyerEmail= User.FindFirstValue(ClaimTypes.Email);
            
            var result = await serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);
            return Ok(result);
        }
    }
}
