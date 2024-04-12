namespace WebShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using System.Diagnostics;

    using Models;
    using Services.Contracts;
    using Services.Models.BookShop;
    using Services.ServiceControllers;

    public class HomeController : BaseController
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
        public async Task<IActionResult> All(
            int genreId,
            string sortClause,
            int itemsOnPage = 6,
            string searchTerm = "",
            int currentPage = 1)
        {
            var model = new Catalogue();
            var hasParse = Enum.TryParse(typeof(ItemSortClause), sortClause, out var sort);
            var sortBy = sort != null ? (ItemSortClause)sort : ItemSortClause.NameAsc;

            if (itemsOnPage > 24)
            {
                itemsOnPage = 24;
            }

            if (itemsOnPage < 6)
            {
                itemsOnPage = 6;
            }

            model.GenreId = genreId;
            model.SearchTerm = searchTerm;
            model.ItemsOnPage = itemsOnPage;
            model.SortClause = sortBy;
            model.Genres = await _bookShopService.GetCategoryList();

            model.MaxPages = await _bookShopService
                .MaxPages(model.SearchTerm, model.ItemsOnPage, model.GenreId);

            if (currentPage < 1)
            {
                currentPage = 1;
            }

            if (currentPage > model.MaxPages)
            {
                currentPage = model.MaxPages;
            }

            model.CurrentPage = currentPage;

            model.Items = await _bookShopService
                .GetCatalogue(model.SearchTerm, model.ItemsOnPage, model.GenreId, sortBy, currentPage);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int itemId, string returnRoute)
        {
            ViewBag.ReturnRoute = returnRoute;
            var model = await _bookShopService.GetBookInfo(itemId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}