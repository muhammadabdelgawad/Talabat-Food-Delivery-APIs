using Talabat.Application.Abstraction.Models.Orders;

namespace Talabat.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);

        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);

        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();
    }
}
