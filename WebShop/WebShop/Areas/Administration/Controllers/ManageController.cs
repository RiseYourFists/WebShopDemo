
using WebShop.Services.Models.Administration;
using WebShop.Services.Models.Administration.Enumerations;

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

        public async Task<IActionResult> Promotions(string searchTerm)
        {
            var model = new PromotionManageModel()
            {
                SearchTerm = searchTerm,
                Promotions = await _service.GetPromotions(searchTerm)
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddPromotion()
        {
            var model = new PromotionEditorModel()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Action = EditAction.Add,
                Authors = await _service.GetPromotionAuthors(),
                Genres = await _service.GetPromotionGenres()
            };
            return View("PromotionEditor", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditPromotion(int id)
        {
            var model = await _service.GetPromotion(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Action = EditAction.Edit;
            model.Authors = await _service.GetPromotionAuthors();
            model.Genres = await _service.GetPromotionGenres();

            return View("PromotionEditor" ,model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPromotion(PromotionEditorModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;
            if (isValid)
            {
                try
                {
                    isValid = await _service.AddPromotion(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "Something went wrong.");
                    }
                }
                catch (Exception e)
                {
                    isValid = false;
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            if (!isValid)
            {
                model.Authors = await _service.GetPromotionAuthors();
                model.Genres = await _service.GetPromotionGenres();
                model.Action = EditAction.Add;

                return View("PromotionEditor", model);
            }
            return RedirectToAction("Promotions");
        }

        [HttpPost]
        public async Task<IActionResult> EditPromotion(PromotionEditorModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;
            if (isValid)
            {
                try
                {
                    isValid = await _service.EditPromotion(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "Something went wrong.");
                    }
                }
                catch (Exception e)
                {
                    isValid = false;
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            if (!isValid)
            {
                model.Authors = await _service.GetPromotionAuthors();
                model.Genres = await _service.GetPromotionGenres();
                model.Action = EditAction.Edit;

                return View("PromotionEditor", model);
            }
            return RedirectToAction("Promotions");
        }

        public IActionResult Users()
        {
            return View();
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
            model.Action = EditAction.Edit;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookInfoModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                try
                {
                    isValid = await _service.EditBookInfo(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "No changes have been made.");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    isValid = false;
                }
            }

            if (!isValid)
            {
                model.Authors = await _service.GetAuthorsSelectionItem();
                model.Genres = await _service.GetGenresSelectionItem();
                return View(model);
            }

            return RedirectToAction("Books");
        }

        [HttpGet]
        public async Task<IActionResult> EditGenre(int id)
        {
            var model = await _service.GetGenreInfo(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Action = EditAction.Edit;
            return View("GenreEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre(GenreEditorModel model)
        {
            ModelState.Remove(nameof(model.Action));
            bool isValid = ModelState.IsValid;

            if (isValid)
            {
                try
                {
                    isValid = await _service.EditGenre(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "No changes have been made!");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    isValid = false;
                }
            }

            if (!isValid)
            {
                return View("GenreEditor", model);
            }

            return RedirectToAction("Books");
        }

        [HttpGet]
        public async Task<IActionResult> EditAuthor(int id)
        {
            var model = await _service.GetAuthorInfo(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Action = EditAction.Edit;
            return View("AuthorEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAuthor(AuthorEditorModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                try
                {
                    isValid = await _service.EditAuthor(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "No changes have been made!");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    isValid = false;
                }
            }

            if (!isValid)
            {
                return View("AuthorEditor", model);
            }

            return RedirectToAction("Books");
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            var model = new AuthorEditorModel()
            {
                Action = EditAction.Add
            };
            return View("AuthorEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorEditorModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                try
                {
                    isValid = await _service.AddNewAuthor(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "Something went wrong.");
                    }
                }
                catch (Exception e)
                {
                    isValid = false;
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            if (!isValid)
            {
                return View("AuthorEditor", model);
            }

            return RedirectToAction("Books");
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            var model = new GenreEditorModel()
            {
                Action = EditAction.Add
            };

            return View("GenreEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreEditorModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                try
                {
                    isValid = await _service.AddNewGenre(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "Something went wrong.");
                    }
                }
                catch (Exception e)
                {
                    isValid = false;
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            if (!isValid)
            {
                return View("GenreEditor", model);
            }

            return RedirectToAction("Books");
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var model = new BookInfoModel()
            {
                Authors = await _service.GetAuthorsSelectionItem(),
                Genres = await _service.GetGenresSelectionItem(),
                Action = EditAction.Add
            };

            return View("EditBook", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookInfoModel model)
        {
            ModelState.Remove(nameof(model.Action));
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                try
                {
                    isValid = await _service.AddNewBook(model);
                    if (!isValid)
                    {
                        ModelState.AddModelError("Error", "Something went wrong.");
                    }
                }
                catch (Exception e)
                {
                    isValid = false;
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            if (!isValid)
            {
                model.Authors = await _service.GetAuthorsSelectionItem();
                model.Genres = await _service.GetGenresSelectionItem();
                model.Action = EditAction.Add;

                return View("EditBook", model);
            }

            return RedirectToAction("Books");
        }
    }
}
