namespace Talabat.Domain.Entities.Oreders
{
    public class ProductItemOrdered
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PictureUrl { get; set; }
    }
}