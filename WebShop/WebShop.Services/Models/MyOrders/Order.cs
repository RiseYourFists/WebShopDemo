﻿namespace WebShop.Services.Models.MyOrders
{
    using Enumerations;

    public class Order
    {
        public Guid Id { get; set; }

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime OrderedOn { get; set; }

        public DateTime? DeliveredOn { get; set; }

        public List<OrderItem> Items { get; set; } = new();

        public decimal TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string ClientName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
