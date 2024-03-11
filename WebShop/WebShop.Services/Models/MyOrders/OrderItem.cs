namespace WebShop.Services.Models.MyOrders
{
    public class OrderItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string BookCover { get; set; } = string.Empty;
    }
}
