using Talabat.Application.Abstraction.Models.Basket;

namespace Talabat.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<BasketDto> GetCustomerBasketAsync(string id);
        Task<BasketDto> UpdateCustomerBasketAsync(BasketDto basket);
        Task DeleteCustomerBasketAsync(string id);

    }
}
