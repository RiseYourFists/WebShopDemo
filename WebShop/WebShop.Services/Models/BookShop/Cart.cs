namespace WebShop.Services.Models.BookShop
{
    public class Cart
    {
        public List<CartListItem> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
