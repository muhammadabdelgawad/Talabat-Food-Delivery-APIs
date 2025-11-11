using Talabat.Application.Abstraction.Services.Products;

namespace Talabat.Infrastructure.Payment_Service
{
    public class PaymentService(IBasketService basketService, IUnitOfWork unitOfWork, IProductService productService) : IPaymentService
    {
        public async Task<Basket?> CreateeOrUpdatePaymentIntent(string basketId)
        {
            var basket = await basketService.GetCustomerBasketAsync(basketId);

            if (basket == null) throw new Exception("Basket Not Found");

            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await productService.GetProductAsync(item.Id);
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }

            }












        }
    }
}
