namespace WebShop.Services.ServiceControllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Core.Contracts;
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

        public async Task<List<GenreCategoryIcon>> GetCategoryIcons()
        {
            var icons = await _repo
                .AllReadonly<Genre>()
                .OrderBy(g => g.Name)
                .ProjectTo<GenreCategoryIcon>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return icons;
        }

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
                    || EF.Functions.Like(b.Description.ToLower(), $"%{searchTerm}%")
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
                    || EF.Functions.Like(b.Description.ToLower(), $"%{searchTerm}%")
                    || EF.Functions.Like(b.Author.Name.ToLower(), $"%{searchTerm}%"));
            }

            double items = await books.CountAsync();
            double itemCount = Math.Ceiling(items / itemsOnPage);
            return int.Parse(itemCount.ToString(CultureInfo.InvariantCulture));
        }

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

        public async Task<bool> AnyBook(int id)
        {
            return await _repo
                .AllReadonly<Book>()
                .AnyAsync(b => b.Id == id);
        }

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
