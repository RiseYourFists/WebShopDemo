namespace WebShop.Services.ServiceControllers
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using WebShop.Core.Contracts;
    using WebShop.Core.Models.BookShop;
    using WebShop.Services.Contracts;

    public class BookShopUtilityService : IBookShopUtilityService
    {
        private readonly IMapper _mapper;
        private readonly IBookShopRepository _repo;
        public BookShopUtilityService(IMapper mapper, IBookShopRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<string> GetSearchResults(string searchTerm)
        {
            var itemsOnList = 10;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return string.Empty;
            }

            var result = await _repo
                .AllReadonly<Book>()
                .Where(b => 
                    EF.Functions.Like(b.Title, $"%{searchTerm}%") ||
                    EF.Functions.Like(b.Author.Name, $"%{searchTerm}%"))
                .Select(book => new
                {
                    book.Id,
                    book.Title,
                    book.BookCover
                })
                .Take(itemsOnList)
                .ToListAsync();

            if (result.Count == 0)
            {
                return string.Empty;
            }

            var json = JsonConvert.SerializeObject(result);

            return json;
        }
    }
}
