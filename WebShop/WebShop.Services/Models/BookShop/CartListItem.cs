namespace WebShop.Services.Models.BookShop
{
    public class CartListItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string CoverPhoto { get; set; } = string.Empty;
    }
}
