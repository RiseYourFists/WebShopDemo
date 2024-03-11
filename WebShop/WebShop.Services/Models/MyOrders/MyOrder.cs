namespace WebShop.Services.Models.MyOrders
{
    using Enumerations;

    public class MyOrder
    {
        public Guid Id { get; set; }

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string OrderedOn { get; set; } = string.Empty;

        public string DeliveredOn { get; set; } = string.Empty;

        public List<OrderItem> Items { get; set; } = new();

        public string TotalPrice { get; set; } = string.Empty;

        public OrderStatus OrderStatus { get; set; }
    }
}
