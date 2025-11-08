
using Address = Talabat.Domain.Entities.Identity.Address;

namespace Talabat.Infrastructure.Persistence._Identity.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd();

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
