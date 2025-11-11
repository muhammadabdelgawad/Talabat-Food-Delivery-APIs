using Talabat.Domain.Entities.Oreders;
using Talabat.Domain.Entities.Products;

namespace Talabat.Infrastructure.Payment_Service
{
    public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IPaymentService
    {
        public async Task<Basket?> CreateeOrUpdatePaymentIntent(string basketId)
        {
            var basket = await basketRepository.GetAsync(basketId);

            if (basket == null) throw new Exception("Basket Not Found");

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.GetRepositiry<DeliveryMethod,int>().GetAsync(basket.DeliveryMethodId.Value);
                
                if(deliveryMethod is null ) throw new Exception(" Not Found");
                
                basket.ShippingPrice = deliveryMethod!.Cost;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = unitOfWork.GetRepositiry<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if(product is null ) throw new Exception("Product Not Found");
                  
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }

            }







             




        }
    }
}
