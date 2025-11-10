using Microsoft.EntityFrameworkCore;

namespace Talabat.Application.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser> FindUserWithAddress(this UserManager<ApplicationUser> userManager,
            ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Where(u => u.Email == email)
                                              .Include(u => u.Address )
                                              .FirstOrDefaultAsync();
             
            return user!;
        }
    }
}
