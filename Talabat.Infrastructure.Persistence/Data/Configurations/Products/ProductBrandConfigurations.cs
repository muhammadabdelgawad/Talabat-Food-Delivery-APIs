
using Talabat.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data.Configurations.Base;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Products
{
    public class ProductBrandConfigurations : BaseAuditableEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
