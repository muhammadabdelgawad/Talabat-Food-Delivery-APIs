namespace Talabat.Domain.Entities.Oreders
{
    public class OrderItem :BaseAuditableEntity<int>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
