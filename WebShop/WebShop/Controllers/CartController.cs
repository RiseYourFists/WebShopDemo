using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShop.Services.Models.BookShop;
using WebShop.Services.ServiceControllers;

namespace WebShop.App.Controllers
{
    public class CartController : BaseController
    {
        private readonly BookShopService _service;
        private readonly CartService _cartService;

        public CartController(BookShopService service, CartService cartService)
        {
            _service = service;
            _cartService = cartService;
        }

        [HttpPost]
        public IActionResult Remove(int id, string returnUrl)
        {
            var cart = GetCart();

            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            if (cart.ContainsKey(id))
            {
                cart.Remove(id);
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            returnUrl = returnUrl.Replace("amp;", "");
            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id, int quantity, string returnRoute)
        {
            var cart = GetCart();

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

            if (cart.Count == 1)
            {
                TempData["UnfoldCart"] = true;
            }


            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Details", "Home", new { ItemId = id, ReturnRoute = returnRoute });
        }

        public IActionResult Clear()
        {
            HttpContext.Session.SetString("Cart", string.Empty);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            var model = new Cart();
            model.Items = await _cartService.GetShopItems(cart);
            model.TotalPrice = model.Items.Sum(i => i.TotalPrice);

            if (model.Items.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Order()
        {
            //TODO: Implement functionality.
            return View();
        }

        private Dictionary<int, int> GetCart()
        {
            var data = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrWhiteSpace(data))
            {
                return new();
            }

            var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(data);

            if (cart == null)
            {
                return new();
            }

            return cart;
        }
    }
}
