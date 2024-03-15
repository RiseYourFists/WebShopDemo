using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.App.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    [Authorize(Roles = "Admin")]
    public class Manage : Controller
    {
        public IActionResult Books()
        {
            return View();
        }

        public IActionResult Promotions()
        {
            return Ok();
        }

        public IActionResult Users()
        {
            return Ok();
        }
    }
}
