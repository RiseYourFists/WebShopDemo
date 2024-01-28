using WebShop.Services.Contracts;

namespace WebShop.Services.Models.BookShop
{
    public class Catalogue
    {
        public int CurrentPage { get; set; }

        public int MaxPages { get; set; }

        public int ItemsOnPage { get; set; }

        public int GenreId { get; set; }

        public string SearchTerm { get; set; }

        public ItemSortClause SortClause { get; set; }

        public List<ItemCard> Items { get; set; } = new();
    }
}
