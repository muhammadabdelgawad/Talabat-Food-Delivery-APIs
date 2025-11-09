namespace Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbInitializer(StoreIdentityDbConetxt _dbContext ,UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext), IStoreIdentityInializer
    {
        public override async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    DisplayName = "Muhammad Abdelgawad",
                    UserName = "muhammad.abdelgawad",
                    Email = "muhammad@outlook.com",
                    PhoneNumber = "01000000100"
                };

                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }

        }
    }
}
