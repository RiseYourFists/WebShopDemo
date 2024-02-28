using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShop.Services.ServiceControllers;

namespace WebShop.App.Controllers
{
    public class CartController : BaseController
    {
        private readonly BookShopService _service;

        public CartController(BookShopService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var data = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrWhiteSpace(data))
            {
                return RedirectToAction("Index", "Home");
            }

            var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(data);
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            if (cart.ContainsKey(id))
            {
                cart.Remove(id);
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id, int quantity)
        {
            var data = HttpContext.Session.GetString("Cart");
            var cart = (!string.IsNullOrWhiteSpace(data))
                ? JsonConvert.DeserializeObject<Dictionary<int, int>>(data)
                : new Dictionary<int, int>();

            if (quantity <= 0)
            {
                quantity = 1;
            }

            if (!await _service.AnyBook(id))
            {
                return RedirectToAction("Index", "Error", 404);
            }

            if (cart.ContainsKey(id))
            {
                cart[id] += quantity;
            }
            else
            {
                cart.Add(id, quantity);
            }


            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Checkout()
        {
            return Ok();
        }
    }
}
