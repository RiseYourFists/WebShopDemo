using WebShop.App.Models.ComponentModels;

namespace WebShop.App.Areas.Employee.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Area(nameof(Employee))]
    [Authorize(Roles = "Admin, Employee")]
    public class DashboardController : Controller
    {
        public IActionResult Orders()
        {
            return View();
        }
    }
}
