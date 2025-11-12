using Microsoft.Extensions.Options;
using Stripe;
using AutoMapper;
using Talabat.Domain.Entities.Oreders;
using Product = Talabat.Domain.Entities.Products.Product;
using Talabat.Application.Abstraction.Models.Basket;

namespace Talabat.Infrastructure.Payment_Service
{
    public class PaymentService(IMapper mapper,
        IBasketRepository basketRepository, 
        IUnitOfWork unitOfWork,
        IOptions<RedisSettings> redisSettings,
        IOptions<StripeSettings>stripeSettings
         ): IPaymentService
    {
        private readonly RedisSettings _redisSettings = redisSettings.Value;
        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await basketRepository.GetAsync(basketId);

            if (basket == null) throw new Exception("Basket Not Found");

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.GetRepositiry<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);

                if (deliveryMethod is null) throw new Exception(" Not Found");

                basket.ShippingPrice = deliveryMethod!.Cost;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = unitOfWork.GetRepositiry<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is null) throw new Exception("Product Not Found");

                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }

            }

            PaymentIntent? paymentIntent = null;
            var paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * (item.Price * 100)) + (long)(basket.ShippingPrice * 100),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "Card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
                
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * (item.Price * 100)) + (long)(basket.ShippingPrice * 100)
                };
                
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, Options);

            }
           
            await basketRepository.UpdateAsync(basket,TimeSpan.FromDays(_redisSettings.TimeToLiveInDays));

            return mapper.Map<BasketDto>(basket);
        }
    }
}