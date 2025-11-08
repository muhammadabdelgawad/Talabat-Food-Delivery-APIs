using Talabat.Application.Abstraction.Models.Common;

namespace Talabat.Application.Abstraction.Models.Orders
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public  int DeliveryMethodId { get; set; }
        public required AddressDto ShippingAddress { get; set; }
    }
}
