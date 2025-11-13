
namespace AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Users = await userManager.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                DisplayName = u.DisplayName,
                Roles = userManager.GetRolesAsync(u).Result
            }).ToListAsync();

            return View(Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var AllRoles = await roleManager.Roles.ToListAsync();
            var viewModel = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = AllRoles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    isSelected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList(),
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (userRole.Any(r => r == role.Name) && !role.isSelected)
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                if (!userRole.Any(r => r == role.Name) && role.isSelected)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
