namespace Talabat.Domain.Entities.Basket
{
    public class Basket :BaseEntity<string>
    {
        public required ICollection<BasketItem> Items { get; set; } = new HashSet<BasketItem>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
 