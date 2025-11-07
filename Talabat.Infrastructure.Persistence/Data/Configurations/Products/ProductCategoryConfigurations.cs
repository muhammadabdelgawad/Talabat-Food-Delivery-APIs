using Talabat.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data.Configurations.Base;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Products
{
    public class ProductCategoryConfigurations: BaseAuditableEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
