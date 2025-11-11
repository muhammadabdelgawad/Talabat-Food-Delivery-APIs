using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Controllers.Base;
using Talabat.Application.Abstraction.Models.Basket;
using Talabat.Application.Abstraction.Services;

namespace Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController :BaseApiController
    {
        private readonly IServiceManager _serviceManager;
        public BasketController(IServiceManager servicesManager) 
            :base(servicesManager)
        {
            _serviceManager = servicesManager;

        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string id )
        {
            var basket = await _serviceManager.BasketService.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basket)
        {
            var updatedBasket = await _serviceManager.BasketService.UpdateCustomerBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteCustomerBasketAsync(id);
            return NoContent();
        }


    }
}
