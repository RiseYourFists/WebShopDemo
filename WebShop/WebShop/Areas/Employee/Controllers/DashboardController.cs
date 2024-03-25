
using System.Globalization;
using System.Text;
using WebShop.App.Areas.Employee.Models;
using WebShop.Services.Models.MyOrders;
using WebShop.Services.Models.MyOrders.Enumerations;
using WebShop.Services.ServiceControllers;

namespace WebShop.App.Areas.Employee.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Area(nameof(Employee))]
    [Authorize(Roles = "Admin, Employee")]
    public class DashboardController : Controller
    {
        private readonly EmployeeService _employeeService;
        public DashboardController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Orders(
            string status,
            string from,
            string to,
            string order,
            int currentPage = 1)
        {
            OrderStatus orderStatus;
            DateTime fromDate;
            DateTime toDate;
            OrderClause orderClause;

            orderStatus = status switch
            {
                nameof(OrderStatus.Delivered) => OrderStatus.Delivered,
                nameof(OrderStatus.Shipped) => OrderStatus.Shipped,
                _ => OrderStatus.Pending
            };

            var isFromValid = DateTime.TryParse(from, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
            var isToValid = DateTime.TryParse(to, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);

            orderClause = order switch
            {
                nameof(OrderClause.OrderDateAsc) => OrderClause.OrderDateAsc,
                nameof(OrderClause.TotalPriceDesc) => OrderClause.TotalPriceDesc,
                nameof(OrderClause.TotalPriceAsc) => OrderClause.TotalPriceAsc,
                _ => OrderClause.OrderDateDesc
            };

            var itemsOnPage = 10;
            int lastPage = 1;

            List<Order> orders = new();

            if (isFromValid && isToValid && fromDate < toDate)
            {
                lastPage = await _employeeService.GetLastPage(orderStatus, fromDate, toDate, itemsOnPage);
                orders = await _employeeService.GetOrders(orderStatus, fromDate, toDate, itemsOnPage, currentPage, orderClause);
            }
            else
            {
                lastPage = await _employeeService.GetLastPage(orderStatus, null, null, itemsOnPage);
                orders = await _employeeService.GetOrders(orderStatus, null, null, itemsOnPage, currentPage, orderClause);
            }

            var model = new OrderPageModel()
            {
                CurrentPage = currentPage,
                From = string.IsNullOrWhiteSpace(from)? null : fromDate,
                To = string.IsNullOrWhiteSpace(to)? null : toDate,
                LastPage = lastPage,
                Orders = orders,
                Order = orderClause,
                Status = orderStatus
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsShipped(string id)
        {
            bool isValid = Guid.TryParse(id, out var guid);
            var sb = new StringBuilder();

            if (!isValid)
            {
                return RedirectToAction("Index", "Error", new { statusCode = 400 });
            }

            try
            {
                isValid = await _employeeService.MarkAsShipped(guid);
                if (!isValid)
                {
                    sb.AppendLine("Something went wrong.");
                }
            }
            catch (Exception e)
            {
                isValid = false;
                sb.AppendLine(e.Message);
            }

            if (!isValid)
            {
                RedirectToAction("DetailedError", "Error", new { message = sb.ToString()});
            }

            return RedirectToAction("Orders", new {status = OrderStatus.Shipped.ToString()});
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsDelivered(string id)
        {
            bool isValid = Guid.TryParse(id, out var guid);
            var sb = new StringBuilder();

            if (!isValid)
            {
                return RedirectToAction("Index", "Error", new { statusCode = 400 });
            }

            try
            {
                isValid = await _employeeService.MarkAsDelivered(guid);
                if (!isValid)
                {
                    sb.AppendLine("Something went wrong.");
                }
            }
            catch (Exception e)
            {
                isValid = false;
                sb.AppendLine(e.Message);
            }

            if (!isValid)
            {
                RedirectToAction("DetailedError", "Error", new { message = sb.ToString() });
            }

            return RedirectToAction("Orders", new { status = OrderStatus.Delivered.ToString() });
        }
    }
}
