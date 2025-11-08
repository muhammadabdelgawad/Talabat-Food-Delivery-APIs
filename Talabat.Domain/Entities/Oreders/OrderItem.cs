namespace Talabat.Domain.Entities.Oreders
{
    public class OrderItem :BaseAuditableEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered product,decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public required ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
