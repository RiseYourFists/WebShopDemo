namespace WebShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using WebShop.Services.Models.BookShop;
    using Services.ServiceControllers;
    using WebShop.Core.Models.Identity;

    public class CartController : BaseController
    {
        private readonly BookShopService _service;
        private readonly CartService _cartService;
        private readonly UserHelper<ApplicationUser, Guid> _userHelper;

        public CartController(BookShopService service, CartService cartService, UserHelper<ApplicationUser, Guid> userHelper)
        {
            _service = service;
            _cartService = cartService;
            _userHelper = userHelper;
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
            ClearCart();

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
            var order = new OrderModel()
            {
                TotalPrice = await _cartService.GetTotalPrice(GetCart())
            };
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderModel model)
        {
            var isValid = ModelState.IsValid;
            var userId = await _userHelper.GetUserId(User);

            if (isValid)
            {
                try
                {

                    var cart = GetCart();

                    if (cart.Count == 0)
                    {
                        ModelState.AddModelError("All", "Invalid cart data!");
                        isValid = false;
                    }
                    else
                    {
                        await _cartService.AddNewOrder(cart, model, userId);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("All", e.Message);
                    isValid = false;
                }

            }

            if (!isValid)
            {
                model.TotalPrice = await _cartService.GetTotalPrice(GetCart());
                return View(model);
            }

            ClearCart();
            var invoice = await _cartService.GetCurrentInvoice(userId);
            return View(nameof(OrderDetails), invoice);
        }

        [HttpGet]
        public IActionResult OrderDetails(Invoice model)
        {
            return View(model);
        }

        private void ClearCart()
        {
            HttpContext.Session.SetString("Cart", string.Empty);
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
