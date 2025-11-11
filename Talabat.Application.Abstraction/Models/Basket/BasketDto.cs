namespace Talabat.Application.Abstraction.Models.Basket
{
    public record BasketDto
    {
        public required string Id { get; set; }
        public required List<BasketItemDto> Items { get; set; }  = new List<BasketItemDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
