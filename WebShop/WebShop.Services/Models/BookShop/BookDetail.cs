namespace WebShop.Services.Models.BookShop
{
    public class BookDetail
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public string BookCover { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;
    }
}
