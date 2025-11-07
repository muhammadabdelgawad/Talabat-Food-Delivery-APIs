namespace Talabat.Infrastructure.Persistence._Data.Configurations.Orders
{
    public class DeliveryMethodConfigurations:BaseEntityConfigurations<DeliveryMethod,int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);
           
            builder.Property(x=>x.Cost)
                .HasColumnType("decimal(8,2)");
        }
    }
}
