using Talabat.Domain.Entities.Orders;
using Address = Talabat.Domain.Entities.Oreders.Address; 

namespace Talabat.Application.Services.Order
{
    public class OrderService(IMapper mapper,IBasketService basketService, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            var basket = await basketService.GetCustomerBasket(order.BasketId);

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count() > 0)
            {
                var productRepo= unitOfWork.GetRepositiry<Product,int>();
                foreach (var item in basket.Items)
                {
                    var product= await productRepo.GetAsync(item.Id);
                    if(product is not null) 
                    {
                        var productItemOrdered = new ProductItemOrdered
                        {
                            Id = product.Id,
                            Name = product.Name,
                            PictureUrl = product.PictureUrl ?? "",
                        };
                        var ordetItem = new OrderItem
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity
                        };

                        orderItems.Add(ordetItem);
                    }
                }
            }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var address= mapper.Map<Address>(order.ShippingAddress);

            var orderToCreate = new Domain.Entities.Orders.Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                DeliveryMethodId = order.DeliveryMethodId,
                Items = orderItems,
                Subtotal = subTotal
            };
            await unitOfWork.GetRepositiry<Domain.Entities.Orders.Order,int>().AddAsync(orderToCreate);

            var created = await unitOfWork.CompleteAsync() > 0;
            if (!created) throw new BadRequestException("Problem in creating order");
               
            return mapper.Map<OrderToReturnDto>(orderToCreate);

        }

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
