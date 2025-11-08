namespace Talabat.Domain.Entities.Orders
{
    public class ProductItemOrdered
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PictureUrl { get; set; }
    }
}