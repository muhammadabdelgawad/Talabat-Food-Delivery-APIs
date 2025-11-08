using Talabat.Domain.Entities.Orders;
using Talabat.Domain.Specifications.Order;
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
        
        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderSpec = new OrderSpecifications(buyerEmail);
            var orders=  await unitOfWork.GetRepositiry<Domain.Entities.Orders.Order,int>()
                                       .GetAllWithSpecAsync(orderSpec);
            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);

        }
      
        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpec = new OrderSpecifications(buyerEmail, orderId);
            var order= await unitOfWork .GetRepositiry<Domain.Entities.Orders.Order,int>()
                                       .GetWithSpecAsync(orderSpec);
            if(order is null) throw new NotFoundException(nameof(order) ,orderId);
            return mapper.Map<OrderToReturnDto>(order);
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            
        }


    }
}
