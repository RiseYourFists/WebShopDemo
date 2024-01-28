
using Microsoft.DotNet.MSIdentity.Shared;
using WebShop.Services.Contracts;
using WebShop.Services.Models.BookShop;
using WebShop.Services.ServiceControllers;

namespace WebShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using System.Diagnostics;
    using Models;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookShopService _bookShopService;
        public HomeController(ILogger<HomeController> logger, BookShopService service)
        {
            _logger = logger;
            _bookShopService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new IndexPage()
            {
                TopFive = await _bookShopService.GetTopFiveOffers(),
                Genres = await _bookShopService.GetCategoryIcons()
            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(int genreId, string sortClause, int itemsOnPage = 6, string searchTerm = "",  int currentPage = 1 )
        {
            var model = new Catalogue();
            var hasParse = Enum.TryParse(typeof(ItemSortClause), sortClause, out var sort);
            var sortBy = sort != null? (ItemSortClause)sort : ItemSortClause.NameAsc;

            model.CurrentPage = currentPage;
            model.GenreId = genreId;
            model.SearchTerm = searchTerm;
            model.ItemsOnPage = itemsOnPage;
            model.SortClause = sortBy;

            model.MaxPages = await _bookShopService
                .MaxPages(model.SearchTerm, model.ItemsOnPage, model.GenreId);

            model.Items = await _bookShopService
                .GetCatalogue(model.SearchTerm, model.ItemsOnPage, model.GenreId, sortBy, currentPage);

            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}