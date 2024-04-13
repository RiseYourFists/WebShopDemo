namespace WebShop.Services.ServiceControllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Core.Contracts;
    using Models.Shared;
    using Models.BookShop;
    using System.Globalization;
    using WebShop.Core.Models.BookShop;
    public class BookShopService
    {
        private readonly IBookShopRepository _repo;
        private readonly IMapper _mapper;
        public BookShopService(IBookShopRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets 5 of the cheapest books. Promotions are being accounted as well.
        /// </summary>
        /// <returns>Task&lt;List&lt;ItemCard&gt;&gt;</returns>
        public async Task<List<ItemCard>> GetTopFiveOffers()
        {
            var books = await _repo
                .AllReadonly<Book>()
                .Where(b => b.StockQuantity > 0)
                .ToListAsync();

            var result = books
                .Select(b => new ItemCard
                {
                    Id = b.Id,
                    Title = b.Title,
                    BasePrice = b.BasePrice,
                    CurrentPrice = b.BasePrice * (1 - (GetPromotion(b.GenreId, b.AuthorId).Result / 100)),
                    BookCover = b.BookCover
                })
             .OrderBy(b => b.CurrentPrice)
             .Take(5)
             .ToList();

            return result;
        }

        /// <summary>
        /// Gets all genres for the home page.
        /// </summary>
        /// <returns>Task&lt;List&lt;GenreCategoryIcon&gt;&gt;</returns>
        public async Task<List<GenreCategoryIcon>> GetCategoryIcons()
        {
            var icons = await _repo
                .AllReadonly<Genre>()
                .OrderBy(g => g.Name)
                .ProjectTo<GenreCategoryIcon>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return icons;
        }

        /// <summary>
        /// Gets all the genres and adds a genre with Id of 0 for all genres
        /// </summary>
        /// <returns>Task&lt;List&lt;DropdownListElement&gt;&gt;</returns>
        public async Task<List<DropdownListElement>> GetCategoryList()
        {
            var genres = await _repo
                .AllReadonly<Genre>()
                .OrderBy(g => g.Name)
                .Select(g => new DropdownListElement()
                {
                    ButtonContent = g.Name,
                    Parameters = new()
                    {
                        { "GenreId", g.Id }
                    },
                    ButtonClasses = "fas fa-book"
                })
                .ToListAsync();

            genres.Insert(0, new DropdownListElement()
            {
                ButtonContent = "All",
                Parameters = new()
                {
                    { "GenreId", 0 }
                },
                ButtonClasses = "fas fa-book"
            });
            return genres;
        }

        /// <summary>
        /// Gets all books filtered by a set of parameters.
        /// </summary>
        /// <param name="searchTerm">Filters books by matching the term with Title and Author name</param>
        /// <param name="itemsOnPage">Determines how many books a page can have</param>
        /// <param name="genreId">Takes only matching genreIds, if Id == 0 it skips filtering.</param>
        /// <param name="sortBy">Sorts the books by name or price asc/desc.</param>
        /// <param name="currentPage">Gets a corresponding page.</param>
        /// <returns>Task&lt;List&lt;ItemCard&gt;&gt;</returns>
        public async Task<List<ItemCard>> GetCatalogue(string searchTerm, int itemsOnPage, int genreId, ItemSortClause sortBy, int currentPage)
        {
            var books = _repo.AllReadonly<Book>()
                .Where(b => b.StockQuantity > 0);

            if (await _repo.AllReadonly<Genre>().AnyAsync(g => g.Id == genreId))
            {
                books = books.Where(b => b.GenreId == genreId);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b =>
                    EF.Functions.Like(b.Title.ToLower(), $"%{searchTerm}%")
                    || EF.Functions.Like(b.Author.Name.ToLower(), $"%{searchTerm}%"));
            }

            var result = books
                .ToList()
                .Select(b => new ItemCard
                {
                    Id = b.Id,
                    Title = b.Title,
                    BasePrice = b.BasePrice,
                    CurrentPrice = b.BasePrice * (1 - (GetPromotion(b.GenreId, b.AuthorId).Result / 100)),
                    BookCover = b.BookCover
                })
            .ToList();


            result = sortBy switch
            {
                ItemSortClause.NameAsc =>
                    result.OrderBy(b => b.Title).ToList(),
                ItemSortClause.NameDesc =>
                    result.OrderByDescending(b => b.Title).ToList(),
                ItemSortClause.PriceAsc =>
                    result.OrderBy(b => b.CurrentPrice).ToList(),
                ItemSortClause.PriceDesc =>
                    result.OrderByDescending(b => b.CurrentPrice).ToList(),
                _ => result.OrderBy(b => b.Id).ToList()
            };

            var maxPages = await this.MaxPages(searchTerm, itemsOnPage, genreId);

            if (currentPage < 1)
            {
                currentPage = 1;
            }

            if (maxPages > 0 && currentPage > maxPages)
            {
                currentPage = maxPages;
            }

            if (itemsOnPage < 1 || itemsOnPage > 24)
            {
                itemsOnPage = 24;
            }

            var skipCount = itemsOnPage * (currentPage - 1);

            result = result
                .Skip(skipCount)
                .Take(itemsOnPage)
                .ToList();

            return result;
        }

        /// <summary>
        /// Returns the last possible page of the search result.
        /// </summary>
        /// <param name = "searchTerm" > Filters books by matching the term with Title and Author name</param>
        /// <param name="itemsOnPage">Determines how many books a page can have</param>
        /// <param name="genreId">Takes only matching genreIds, if Id == 0 it skips filtering.</param>
        /// <returns></returns>
        public async Task<int> MaxPages(string searchTerm, int itemsOnPage, int genreId)
        {
            var books = _repo.AllReadonly<Book>();

            if (await _repo.AllReadonly<Genre>().AnyAsync(g => g.Id == genreId))
            {
                books = books.Where(b => b.GenreId == genreId);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b =>
                    EF.Functions.Like(b.Title.ToLower(), $"%{searchTerm}%")
                    || EF.Functions.Like(b.Author.Name.ToLower(), $"%{searchTerm}%"));
            }

            double items = await books.CountAsync();
            double itemCount = Math.Ceiling(items / itemsOnPage);
            return int.Parse(itemCount.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets full book info by its key identifier for display.
        /// Returns null if there are no results.
        /// </summary>
        /// <param name="id">Key identifier</param>
        /// <returns>Task&lt;BookDetail?&gt;</returns>
        public async Task<BookDetail?> GetBookInfo(int id)
        {
            var book = await _repo
                .AllReadonly<Book>()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.Id == id && b.StockQuantity > 0)
                .ToListAsync();

            var result = book
                .Select(b => new BookDetail()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    BasePrice = b.BasePrice,
                    CurrentPrice = (b.BasePrice * (1 - (GetPromotion(b.GenreId, b.AuthorId).Result / 100))),
                    BookCover = b.BookCover,
                    Genre = b.Genre.Name,
                    Author = b.Author.Name
                })
                .FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Checks if a book with the specified id exists in the context.
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> AnyBook(int id)
        {
            return await _repo
                .AllReadonly<Book>()
                .AnyAsync(b => b.Id == id);
        }

        /// <summary>
        /// Gets the promotion discount percent if there's an existing promotion otherwise it returns 0.
        /// </summary>
        /// <param name="genreId">Book's genre id.</param>
        /// <param name="authorId">Book's author id</param>
        /// <returns>Task&lt;decimal&gt;</returns>
        private async Task<decimal> GetPromotion(int genreId, int authorId)
        {

            var promotion = await _repo
                .AllReadonly<Promotion>()
                .Include(p => p.AuthorPromotions)
                .Include(p => p.GenrePromotions)
                .Where(p =>
                    (p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now) &&
                    (p.GenrePromotions
                         .Any(gp => gp.GenreId == genreId) ||
                     p.AuthorPromotions
                         .Any(ap => ap.AuthorId == authorId))
                )
                .Select(p => (decimal?)p.DiscountPercent)
                .FirstOrDefaultAsync();
            return promotion != null ? promotion.Value : 0;
        }
    }
}
