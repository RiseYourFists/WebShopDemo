namespace WebShop.App.Controllers
{
    using System.Globalization;
    using Microsoft.AspNetCore.Mvc;
    using Services.ServiceControllers;
    using WebShop.Services.Models.MyOrders;
    using WebShop.Services.Models.MyOrders.Enumerations;
    public class MyOrdersController : BaseController
    {
        private readonly MyOrderService _myOrderService;
        public MyOrdersController(MyOrderService myOrderService)
        {
            _myOrderService = myOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string status, string from, string to, string orderClause)
        {
            var orderStatus = status switch
            {
                "Pending" => OrderStatus.Pending,
                "Shipped" => OrderStatus.Shipped,
                "Delivered" => OrderStatus.Delivered,
                _ => OrderStatus.All
            };

            var orderValue = orderClause switch
            {
                "TotalPriceAsc" => OrderClause.TotalPriceAsc,
                "TotalPriceDesc" => OrderClause.TotalPriceDesc,
                "OrderDateAsc" => OrderClause.OrderDateAsc,
                _ => OrderClause.OrderDateDesc
            };

            var isFromDateValid = DateTime.TryParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var fromDate);

            var isToDateValid = DateTime.TryParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var toDate);

            DateTime? fromValue = isFromDateValid ? fromDate : null;
            DateTime? toValue = isToDateValid ? toDate : null;

            var model = new MyOrdersPage()
            {
                OrderStatus = orderStatus,
                OrderClause = orderValue,
                From = fromValue,
                To = toValue,
                HasAnyOrders = await _myOrderService.AnyUserOrdersPresent(User),
                Orders = await _myOrderService.GetUserOrders(User, orderStatus, orderValue, fromValue, toValue)
            };
            return View(model);
        }
    }
}
