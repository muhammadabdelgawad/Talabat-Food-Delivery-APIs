namespace Talabat.Infrastructure.Persistence._Data.Configurations.Orders
{
    public class OrederConfigurations :BaseEntityConfigurations<Order,int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            
            builder.OwnsOne(o => o.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.Property(Order => Order.Status)
                .HasConversion(
                    (status) => status.ToString(),
                    (status) => (OrderStatus)Enum.Parse(typeof(OrderStatus), status)
                );

            builder.Property(o => o.OrderDate)
                .HasColumnType("decimal(8,2)");

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
