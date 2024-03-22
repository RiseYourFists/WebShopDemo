namespace WebShop.Services.Models.BookShop
{
    public class IndexPage
    {
        public List<ItemCard> TopFive { get; set; } = new();

        public List<GenreCategoryIcon> Genres { get; set; } = new();
    }
}
