namespace Talabat.Domain.Entities.Oreders
{
    public class OrderItem :BaseAuditableEntity<int>
    {
        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
