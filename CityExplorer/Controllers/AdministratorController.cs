using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManager()
        {
            return RedirectToAction("Index", "RoleManager");
        }

        public IActionResult UserRoles()
        {
            return RedirectToAction("Index", "UserRoles");
        }

    }
}
