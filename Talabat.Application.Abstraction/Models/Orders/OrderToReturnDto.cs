using Talabat.Application.Abstraction.Models.Common;
namespace Talabat.Application.Abstraction.Models.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; } 
        public required AddressDto ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; }
        public string ? DeliveryMethod { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; set; }

        public decimal GetTotal() => Subtotal + DeliveryMethod!.Cost;

        public string PaymentIntentId { get; set; } = "";
    }
}
