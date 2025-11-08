namespace Talabat.Application.Abstraction.Models.Orders
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
