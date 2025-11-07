using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Talabat.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Identity.Configurations;

namespace Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbConetxt : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbConetxt(DbContextOptions<StoreIdentityDbConetxt> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());
        }


          
        }
}
  