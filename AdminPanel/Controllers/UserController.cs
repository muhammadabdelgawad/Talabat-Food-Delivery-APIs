using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
