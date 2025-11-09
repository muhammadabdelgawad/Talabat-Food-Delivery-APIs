using Talabat.Domain.Entities.Identity;
using UserAddress = Talabat.Domain.Entities.Identity.UserAddress;

namespace Talabat.Infrastructure.Persistence._Identity.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           
            builder.Property(u=>u.DisplayName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(100);


            builder.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<UserAddress>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Users");
        }
    }
}
