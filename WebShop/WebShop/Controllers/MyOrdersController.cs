namespace WebShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebShop.Services.Models.MyOrders;
    using WebShop.Services.ServiceControllers;
    public class MyOrdersController : BaseController
    {
        private readonly MyOrderService _myOrderService;
        public MyOrdersController(MyOrderService myOrderService)
        {
            _myOrderService = myOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new MyOrdersPage()
            {
                Orders = await _myOrderService.GetUserOrders(User)
            };
            return View(model);
        }
    }
}
