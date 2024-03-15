namespace WebShop.Services.Models.Administration
{
    public class BookListItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public int StockQuantity { get; set; }

        public string CoverPhoto { get; set; } = string.Empty;
    }
}
