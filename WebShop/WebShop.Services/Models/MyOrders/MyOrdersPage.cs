using WebShop.Services.Models.MyOrders.Enumerations;

namespace WebShop.Services.Models.MyOrders
{
    public class MyOrdersPage
    {
        public bool HasAnyOrders { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderClause OrderClause { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
