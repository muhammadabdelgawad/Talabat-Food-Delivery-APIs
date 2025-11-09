
using UserAddress = Talabat.Domain.Entities.Identity.UserAddress;

namespace Talabat.Infrastructure.Persistence._Identity.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(nameof(UserAddress.Id)).ValueGeneratedOnAdd();

            builder.Property(a => a.FirstName)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.Property(a => a.LastName)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);
                  
            builder.Property(a => a.Street)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.Property(a => a.City)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.Property(a => a.Country)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);
                  

            builder.ToTable("Addresses");

        }
    }
}
