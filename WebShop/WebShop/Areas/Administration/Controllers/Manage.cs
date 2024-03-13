using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.App.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    [Authorize(Roles = "Admin")]
    public class Manage : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
