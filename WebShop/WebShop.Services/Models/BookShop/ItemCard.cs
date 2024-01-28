namespace WebShop.Services.Models.BookShop
{
    public class ItemCard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public string BookCover { get; set; }
    }
}
