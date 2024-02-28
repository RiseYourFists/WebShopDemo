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
                .OrderBy(b => b.CurrentPrice)
                .Take(5)
                .ProjectTo<ItemCard>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return books;
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
            var books = _repo.AllReadonly<Book>();

            if (await _repo.AllReadonly<Genre>().AnyAsync(g => g.Id == genreId))
            {
                books = books.Where(b => b.GenreId == genreId);
            }

            books = sortBy switch
            {
                ItemSortClause.NameAsc => books.OrderBy(b => b.Title),
                ItemSortClause.NameDesc => books.OrderByDescending(b => b.Title),
                ItemSortClause.PriceAsc => books.OrderBy(b => b.CurrentPrice),
                ItemSortClause.PriceDesc => books.OrderByDescending(b => b.CurrentPrice),
                _ => books.OrderBy(b => b.Id)
            };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b =>
                    EF.Functions.Like(b.Title.ToLower(), $"%{searchTerm}%")
                    || EF.Functions.Like(b.Description.ToLower(), $"%{searchTerm}%")
                    || EF.Functions.Like(b.Author.Name.ToLower(), $"%{searchTerm}%"));
            }

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

            books = books
                .Skip(skipCount)
                .Take(itemsOnPage);

            return await books.ProjectTo<ItemCard>(_mapper.ConfigurationProvider)
                .ToListAsync();
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
            return await _repo
                .AllReadonly<Book>()
                .Where(b => b.Id == id)
                .ProjectTo<BookDetail>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AnyBook(int id)
        {
            return await _repo
                .AllReadonly<Book>()
                .AnyAsync(b => b.Id == id);
        }
    }
}
