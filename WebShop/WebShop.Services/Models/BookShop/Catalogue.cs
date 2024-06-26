﻿namespace WebShop.Services.Models.BookShop
{
    using Shared;
    using Contracts;
    public class Catalogue
    {
        public int CurrentPage { get; set; }

        public int MaxPages { get; set; }

        public int ItemsOnPage { get; set; }

        public int GenreId { get; set; }

        public string SearchTerm { get; set; } = string.Empty;

        public ItemSortClause SortClause { get; set; }

        public List<ItemCard> Items { get; set; } = new();

        public List<DropdownListElement> Genres { get; set; } = new();
    }
}
