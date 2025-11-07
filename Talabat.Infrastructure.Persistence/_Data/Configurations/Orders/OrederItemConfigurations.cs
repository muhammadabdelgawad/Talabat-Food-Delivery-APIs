namespace Talabat.Infrastructure.Persistence._Data.Configurations.Orders
{
    public class OrederItemConfigurations : BaseEntityConfigurations<OrderItem, int>

    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(item=>item.Product, Product=>Product.WithOwner());

            builder.Property(item=>item.Price)
                .HasColumnType("decimal(18,2)");
        } 
    }
}
