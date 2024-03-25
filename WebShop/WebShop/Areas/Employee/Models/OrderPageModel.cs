namespace WebShop.App.Areas.Employee.Models
{
    using WebShop.Services.Models.MyOrders;
    using WebShop.Services.Models.MyOrders.Enumerations;

    public class OrderPageModel
    {
        public OrderStatus Status { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public OrderClause Order { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
