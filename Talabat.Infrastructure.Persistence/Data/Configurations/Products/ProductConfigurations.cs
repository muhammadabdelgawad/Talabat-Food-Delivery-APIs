using Talabat.Domain.Entities.Products;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Products
{
    public class ProductConfigurations : BaseAuditableEntityConfigurations<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.NormalizedName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(9,2)");

            builder.HasOne(product => product.Brand)
                .WithMany()
                .HasForeignKey(product => product.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(product => product.Category)
                .WithMany()
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
