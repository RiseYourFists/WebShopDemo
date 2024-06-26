﻿
using Microsoft.AspNetCore.Identity;
using WebShop.Core.Models.Identity;
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
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly AdministrationService _service;
        private readonly UserHelper<ApplicationUser, Guid> _userHelper;

        public ManageController(AdministrationService service, UserHelper<ApplicationUser, Guid> userHelper)
        {
            _service = service;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Books(string searchTerm, int? authorId, int? genreId)
        {

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

        public async Task<IActionResult> Users(string searchTerm)
        {
            var model = new UserPage()
            {
                SearchTerm = searchTerm,
                Users = await _service.GetUsers(searchTerm),
                CurrentUserId = await _userHelper.GetUserId(User)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string userId)
        {
            try
            {
                var currentUserId = await _userHelper.GetUserId(User);
                await _service.PromoteUser(userId, currentUserId);
            }
            catch (Exception e)
            {
                return RedirectToAction("DetailedError", "Error", new { message = e.Message });
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            try
            {
                var currentUserId = await _userHelper.GetUserId(User);
                await _service.DemoteUser(userId, currentUserId);
            }
            catch (Exception e)
            {
                return RedirectToAction("DetailedError", "Error", new { message = e.Message });
            }
            return RedirectToAction("Users");
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
