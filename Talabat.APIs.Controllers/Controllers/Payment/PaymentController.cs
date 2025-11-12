using Talabat.Application.Abstraction.Models.Basket;
using Talabat.Domain.Contracts.Infrastructure;

namespace Talabat.APIs.Controllers.Controllers.Payment
{
    [Authorize]
    public class PaymentController(IPaymentService paymentService) :BaseApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await paymentService.CreateOrUpdatePaymentIntent(basketId);
            return Ok(result);
        }
    }
}
