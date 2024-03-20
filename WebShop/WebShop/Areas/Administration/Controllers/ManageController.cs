
using WebShop.Services.Models.Administration;

namespace WebShop.App.Areas.Administration.Controllers
{
    using System.Globalization;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Services.ServiceControllers;

    [Area(nameof(Administration))]
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly AdministrationService _service;

        public ManageController(AdministrationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Books(string searchTerm, string parameters)
        {
            string paramName = string.Empty;
            int paramId = 0;
            bool isValid = false;

            if (!string.IsNullOrEmpty(parameters))
            {
                var data = parameters.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (data.Length == 2)
                {
                    paramName = data[0];
                    isValid = int.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out paramId);
                }
            }

            int? authorId = null;
            int? genreId = null;

            if (isValid && paramId > 0)
            {
                if (paramName.ToLower() == "genreid")
                {
                    genreId = paramId;
                }

                if (paramName.ToLower() == "authorid")
                {
                    authorId = paramId;
                }
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();
            }

            var model = new BookManageModel()
            {
                Authors = await _service.GetAuthors(),
                Genres = await _service.GetGenres(),
                Books = await _service.GetBooks(searchTerm, authorId, genreId),
                SearchTerm = searchTerm
            };
            return View(model);
        }

        public IActionResult Promotions()
        {
            return Ok();
        }

        public IActionResult Users()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            var model = await _service.GetBookInfo(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Authors = await _service.GetAuthorsSelectionItem();
            model.Genres = await _service.GetGenresSelectionItem();

            return View(model);
        }

        public async Task<IActionResult> EditBook(BookInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await _service.GetAuthorsSelectionItem();
                model.Genres = await _service.GetGenresSelectionItem();
                return View(model);
            }
            return Ok();
        }

        public IActionResult EditGenre(int id)
        {
            return Ok();
        }

        public IActionResult EditAuthor(int id)
        {
            return Ok();
        }

        public IActionResult AddAuthor()
        {
            return Ok();
        }

        public IActionResult AddGenre()
        {
            return Ok();
        }

        public IActionResult AddBook()
        {
            return Ok();
        }
    }
}
