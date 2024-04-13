namespace WebShop.Services.ServiceControllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using System.Text;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Models.Administration;
    using WebShop.Core.Contracts;
    using WebShop.Core.Models.BookShop;
    using WebShop.Core.Models.Identity;
    using static ErrorMessages.AdministrationErrors;

    public class AdministrationService
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdministrationService(
            IMapper mapper,
            IAdminRepository adminRepository,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _adminRepository = adminRepository;
        }

        /// <summary>
        /// Gets all genres and adds a genre with id 0 for all genres.
        /// </summary>
        /// <returns>Task&lt;List&lt;GenreListItem&gt;&gt;</returns>
        public async Task<List<GenreListItem>> GetGenres()
        {
            var result = await _adminRepository
                .AllReadonly<Genre>()
                .ProjectTo<GenreListItem>(_mapper.ConfigurationProvider)
                .ToListAsync();

            result.Insert(0, new GenreListItem()
            {
                Id = 0,
                Name = "All"
            });

            return result;
        }

        /// <summary>
        /// Gets all authors and adds an author with id 0 for all authors.
        /// </summary>
        /// <returns>Task&lt;List&lt;AuthorListItem&gt;&gt;</returns>
        public async Task<List<AuthorListItem>> GetAuthors()
        {
            var result = await _adminRepository
                .AllReadonly<Author>()
                .ProjectTo<AuthorListItem>(_mapper.ConfigurationProvider)
                .ToListAsync();

            result.Insert(0, new AuthorListItem()
            {
                Id = 0,
                Name = "All"
            });

            return result;
        }

        /// <summary>
        /// Returns all books.
        /// </summary>
        /// <param name="searchTerm">Filter that's compared with book titles. (optional)</param>
        /// <param name="authorId">Author id for filtering (optional)</param>
        /// <param name="genreId">Genre id for filtering (optional)</param>
        /// <returns>Task&lt;List&lt;BookListItem&gt;&gt;</returns>
        public async Task<List<BookListItem>> GetBooks(string searchTerm, int? authorId, int? genreId)
        {
            var query = _adminRepository
                .AllReadonly<Book>();


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query
                    .Where(b => EF.Functions.Like(b.Title, $"%{searchTerm}%"));
            }

            if (authorId.HasValue && authorId.Value != 0)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }
            else if (genreId.HasValue && genreId.Value != 0)
            {
                query = query.Where(b => b.GenreId == genreId.Value);
            }

            var books = await query.ToListAsync();

            var result = books
                .Select(b => new BookListItem()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverPhoto = b.BookCover,
                    BasePrice = b.BasePrice,
                    CurrentPrice = b.BasePrice * (1 - GetPromotion(_adminRepository, b.GenreId, b.AuthorId).Result / 100),
                    StockQuantity = b.StockQuantity
                })
                .ToList();

            return result;
        }

        /// <summary>
        /// Gets all genres for the custom select element.
        /// </summary>
        /// <returns>Task&lt;List&lt;SelectionItemModel&gt;&gt;</returns>
        public async Task<List<SelectionItemModel>> GetGenresSelectionItem()
        {
            var genres = await _adminRepository
                .AllReadonly<Genre>()
                .Select(g => new SelectionItemModel()
                {
                    PropertyName = g.Name,
                    PropertyValue = g.Id
                })
                .ToListAsync();

            return genres;
        }

        /// <summary>
        /// Gets all authors for the custom select element.
        /// </summary>
        /// <returns>Task&lt;List&lt;SelectionItemModel&gt;&gt;</returns>
        public async Task<List<SelectionItemModel>> GetAuthorsSelectionItem()
        {
            var authors = await _adminRepository
                .AllReadonly<Author>()
                .Select(a => new SelectionItemModel()
                {
                    PropertyName = a.Name,
                    PropertyValue = a.Id
                })
                .ToListAsync();

            return authors;
        }

        /// <summary>
        /// Gets whole info of a book by its corresponding id.
        /// If nothing is found null is returned
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;BookInfoModel?&gt;</returns>
        public async Task<BookInfoModel?> GetBookInfo(int id)
        {
            var book = await _adminRepository
                .All<Book>()
                .ProjectTo<BookInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }

        /// <summary>
        /// Checks if a genre with the specified id exists.
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> AnyGenre(int id)
        {
            return await _adminRepository
                .AllReadonly<Genre>()
                .AnyAsync(g => g.Id == id);
        }

        /// <summary>
        /// Checks if an author with the specified id exists.
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> AnyAuthor(int id)
        {
            return await _adminRepository
                .AllReadonly<Author>()
                .AnyAsync(a => a.Id == id);
        }

        /// <summary>
        /// Gets a book by its specified identifier and returns a tracked entity that's ready for editing.
        /// Returns null if no book with the specified id is found.
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;Book?&gt;</returns>
        public async Task<Book?> GetBook(int id)
        {
            return await _adminRepository
                .All<Book>()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <summary>
        /// Edits the info of the whole book.
        /// Returns true if all the changes have been applied successfully.
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> EditBookInfo(BookInfoModel model)
        {
            var book = await GetBook(model.Id);

            if (book == null)
            {
                throw new InvalidOperationException(InvalidBookId);
            }

            var doesGenreExist = await AnyGenre(model.GenreId);
            var doesAuthorExist = await AnyAuthor(model.AuthorId);

            if (!doesAuthorExist || !doesGenreExist)
            {
                throw new InvalidOperationException(InvalidGenreAuthorId);
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.BookCover = model.BookCover;
            book.BasePrice = model.BasePrice;
            book.GenreId = model.GenreId;
            book.AuthorId = model.AuthorId;
            book.StockQuantity = model.StockQuantity;

            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Gets the whole info of a genre with a specified id.
        /// Returns null if nothing is found.
        /// </summary>
        /// <param name="id">Key identifier</param>
        /// <returns>Task&lt;GenreEditorModel?&gt;</returns>
        public async Task<GenreEditorModel?> GetGenreInfo(int id)
        {
            return await _adminRepository
                .AllReadonly<Genre>()
                .ProjectTo<GenreEditorModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        /// <summary>
        /// Adds a new genre to the context.
        /// Returns true if it's successfully added.
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> AddNewGenre(GenreEditorModel model)
        {
            var genre = _mapper.Map<Genre>(model);

            await _adminRepository.AddAsync(genre);
            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Edits the info of an existing genre.
        /// Returns true if changes are applied successfully.
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> EditGenre(GenreEditorModel model)
        {
            var doesGenreExist = await AnyGenre(model.Id);
            if (!doesGenreExist)
            {
                throw new InvalidOperationException(InvalidGenreId);
            }

            var genre = await _adminRepository
                .All<Genre>()
                .FirstAsync(g => g.Id == model.Id);

            genre.Name = model.Name;
            genre.IconLink = model.IconLink;

            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Gets the whole info of an author with a specified id.
        /// Returns null if nothing is found.
        /// </summary>
        /// <param name="id">Key identifier</param>
        /// <returns>Task&lt;AuthorEditorModel?&gt;</returns>
        public async Task<AuthorEditorModel?> GetAuthorInfo(int id)
        {
            return await _adminRepository
                .AllReadonly<Author>()
                .ProjectTo<AuthorEditorModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }


        /// <summary>
        /// Adds new author to the context.
        /// Returns true if added successfully.
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> AddNewAuthor(AuthorEditorModel model)
        {
            var author = _mapper.Map<Author>(model);

            await _adminRepository.AddAsync(author);
            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Edits the info of an existing author.
        /// Returns true if changes are applied successfully.
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> EditAuthor(AuthorEditorModel model)
        {
            var doesAuthorExist = await AnyAuthor(model.Id);
            if (!doesAuthorExist)
            {
                throw new InvalidOperationException(InvalidAuthorId);
            }

            var author = await _adminRepository
                .All<Author>()
                .FirstAsync(a => a.Id == model.Id);

            author.Name = model.Name;

            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Adds a new genre to the context.
        /// Returns true if it's successfully added.
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> AddNewBook(BookInfoModel model)
        {
            var doesGenreExist = await AnyGenre(model.GenreId);
            var doesAuthorExist = await AnyAuthor(model.AuthorId);

            if (!doesAuthorExist || !doesGenreExist)
            {
                throw new InvalidOperationException(InvalidGenreAuthorId);
            }

            var book = _mapper.Map<Book>(model);

            await _adminRepository.AddAsync(book);
            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Gets all promotions.
        /// </summary>
        /// <param name="searchTerm">Checks if there's containing element in promotions name</param>
        /// <returns>Task&lt;List&lt;PromotionListItem&gt;&gt;</returns>
        public async Task<List<PromotionListItem>> GetPromotions(string? searchTerm)
        {
            var result = _adminRepository
                .AllReadonly<Promotion>()
                .ProjectTo<PromotionListItem>(_mapper.ConfigurationProvider);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = result.Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%"));
            }

            return await result.ToListAsync();
        }

        /// <summary>
        /// Gets the whole info of a promotion with a specified id.
        /// Returns null if nothing is found.
        /// </summary>
        /// <param name="id">Key identifier</param>
        /// <returns>Task&lt;PromotionEditorModel?&gt;</returns>
        public async Task<PromotionEditorModel?> GetPromotion(int id)
        {
            var result = await _adminRepository
                .AllReadonly<Promotion>()
                .Include(p => p.AuthorPromotions)
                .Include(p => p.GenrePromotions)
                .ProjectTo<PromotionEditorModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }

        /// <summary>
        /// Gets all authors and maps them to custom select element.
        /// All ids are with 'author-' prefix
        /// </summary>
        /// <returns>Task&lt;List&lt;SelectionItemModel&gt;&gt;</returns>
        public async Task<List<SelectionItemModel>> GetPromotionAuthors()
        {
            var authors = await _adminRepository
                .AllReadonly<Author>()
                .Select(a => new SelectionItemModel()
                {
                    PropertyName = a.Name,
                    PropertyValue = "author-" + a.Id
                })
                .ToListAsync();

            return authors;
        }

        /// <summary>
        /// Gets all genres and maps them to custom select element.
        /// All ids are with 'genre-' prefix
        /// </summary>
        /// <returns>Task&lt;List&lt;SelectionItemModel&gt;&gt;</returns>
        public async Task<List<SelectionItemModel>> GetPromotionGenres()
        {
            var genres = await _adminRepository
                .AllReadonly<Genre>()
                .Select(a => new SelectionItemModel()
                {
                    PropertyName = a.Name,
                    PropertyValue = "genre-" + a.Id
                })
                .ToListAsync();

            return genres;
        }


        /// <summary>
        /// Removes all mappings from a promotion by specified id.
        /// Returns true if the action is completed successfully.
        /// </summary>
        /// <param name="promotionId">Key identifier</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> RemovePromotions(int promotionId)
        {
            var genrePromotions = await _adminRepository
                .All<GenrePromotion>()
                .Where(gp => gp.PromotionId == promotionId)
                .ToListAsync();

            var authorPromotions = await _adminRepository
                .All<AuthorPromotion>()
                .Where(gp => gp.PromotionId == promotionId)
                .ToListAsync();

            bool result = false;
            if (genrePromotions.Count > 0 || authorPromotions.Count > 0)
            {
                _adminRepository.DeleteRange(genrePromotions);
                _adminRepository.DeleteRange(authorPromotions);
                result = await _adminRepository.SaveChangesAsync() > 0;
            }

            return result;
        }

        /// <summary>
        /// Applies changes to the whole promotion.
        /// Returns true if changes are applied successfully.
        /// <para>Restrictions:</para>
        /// <para>  -The promotion cannot be added if there are no books to the genre/author assigned.</para>
        /// <para>  -The promotion cannot be added if the books already have other active promotions.</para>
        /// <para>  -The promotion type can only be author/genre-[ID] only can be picked.</para>
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> EditPromotion(PromotionEditorModel model)
        {
            var promotiontokens = model.PromotionType.Split("-");
            var promoType = promotiontokens.First();
            var isPromoIdValid = int.TryParse(promotiontokens.Last(), NumberStyles.None, CultureInfo.InvariantCulture, out var promoId);

            if (isPromoIdValid == false)
            {
                throw new ArgumentException(InvalidPromotionIdFormat);
            }

            var promotion = await _adminRepository
                .All<Promotion>()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (promotion == null)
            {
                throw new InvalidOperationException(InvalidPromotionId);
            }

            Author? author = null;
            Genre? genre = null;

            if (promoType == "author")
            {
                author = await _adminRepository
                    .AllReadonly<Author>()
                    .FirstOrDefaultAsync(a => a.Id == promoId);
            }
            else if (promoType == "genre")
            {
                genre = await _adminRepository
                    .AllReadonly<Genre>()
                    .FirstOrDefaultAsync(g => g.Id == promoId);
            }
            else
            {
                throw new ArgumentException(InvalidPromotionIdFormat);
            }

            if (genre == null && author == null)
            {
                throw new InvalidOperationException(GenreAuthorNotFound);
            }

            await RemovePromotions(promotion.Id);

            promotion.Name = model.Name;
            promotion.DiscountPercent = model.DiscountPercent;
            promotion.StartDate = model.StartDate;
            promotion.EndDate = model.EndDate;

            if (genre != null)
            {
                promotion.GenrePromotions.Add(new GenrePromotion()
                {
                    Genre = genre
                });
            }
            else
            {
                promotion.AuthorPromotions.Add(new AuthorPromotion()
                {
                    Author = author
                });
            }

            if (await IsExistingPromotionInvalid(promotion) == true)
            {
                throw new ArgumentException(ExistingPromotions);
            }

            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Add new promotion to the context.
        /// Returns true if changes are applied successfully.
        /// <para>Restrictions:</para>
        /// <para>  -The promotion cannot be added if there are no books to the genre/author assigned.</para>
        /// <para>  -The promotion cannot be added if the books already have other active promotions.</para>
        /// <para>  -The promotion type can only be author/genre-[ID] only can be picked.</para>
        /// </summary>
        /// <param name="model">DTO model to map the info to the entity.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> AddPromotion(PromotionEditorModel model)
        {
            var promotiontokens = model.PromotionType.Split("-");
            var promoType = promotiontokens.First();
            var isPromoIdValid = int.TryParse(promotiontokens.Last(), NumberStyles.None, CultureInfo.InvariantCulture, out var promoId);

            if (isPromoIdValid == false)
            {
                throw new ArgumentException(InvalidPromotionIdFormat);
            }

            Author? author = null;
            Genre? genre = null;

            if (promoType == "author")
            {
                author = await _adminRepository
                    .AllReadonly<Author>()
                    .FirstOrDefaultAsync(a => a.Id == promoId);
            }
            else if (promoType == "genre")
            {
                genre = await _adminRepository
                    .AllReadonly<Genre>()
                    .FirstOrDefaultAsync(g => g.Id == promoId);
            }
            else
            {
                throw new ArgumentException(InvalidPromotionIdFormat);
            }

            if (genre == null && author == null)
            {
                throw new InvalidOperationException(GenreAuthorNotFound);
            }

            var promotion = new Promotion()
            {
                Name = model.Name,
                DiscountPercent = model.DiscountPercent,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };

            if (genre != null)
            {
                promotion.GenrePromotions.Add(new GenrePromotion()
                {
                    GenreId = genre.Id
                });
            }
            else
            {
                promotion.AuthorPromotions.Add(new AuthorPromotion()
                {
                    AuthorId = author.Id
                });
            }

            if (await IsPreExistingPromotionInvalid(promotion) == true)
            {
                throw new ArgumentException(ExistingPromotions);
            }

            await _adminRepository.AddAsync(promotion);
            var result = await _adminRepository.SaveChangesAsync() > 0;
            return result;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <param name="searchTerm">Checks if the First/Last name, Roles or the Email has a containing element</param>
        /// <returns>Task&lt;List&lt;UserListItem&gt;&gt;</returns>
        public async Task<List<UserListItem>> GetUsers(string searchTerm)
        {

            var result = _adminRepository
                .AllReadonly<ApplicationUser>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new UserListItem()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    Role = u.UserRoles
                        .Select(ur => ur.Role.Name)
                        .FirstOrDefault()
                });

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = result.Where(u =>
                    EF.Functions.Like(u.Email, $"%{searchTerm}%") ||
                    EF.Functions.Like(u.FirstName, $"%{searchTerm}%") ||
                    EF.Functions.Like(u.LastName, $"%{searchTerm}%") ||
                    u.Role != null && EF.Functions.Like(u.Role, $"%{searchTerm}%"));
            }

            return await result.ToListAsync();
        }

        /// <summary>
        /// Promotes the user as follows Guest &gt; Employee &gt; Admin.
        /// Returns true if the action is performed successfully.
        /// <para>Restrictions:
        ///     <para>  -The user cannot perform promotions on him self.</para>
        /// </para>
        /// </summary>
        /// <param name="userId">User for promotion.</param>
        /// <param name="currentUserId">The user who's performing the promotion</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> PromoteUser(string userId, Guid currentUserId)
        {
            var isUserIdValid = Guid.TryParse(userId, out var userGuid);
            if (!isUserIdValid)
            {
                throw new ArgumentException(InvalidUserIdFormat);
            }

            if (userGuid == currentUserId)
            {
                throw new InvalidOperationException(SelfPromotionError);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userGuid);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            var currentUserRoles = await _userManager.GetRolesAsync(user);
            var role = currentUserRoles.FirstOrDefault();

            var result = role switch
            {
                "Employee" => await _userManager.AddToRoleAsync(user, "Admin"),
                null => await _userManager.AddToRoleAsync(user, "Employee"),
                _ => IdentityResult.Failed(new[]
                {
                    new IdentityError(){Description = RoleOverflow}
                })
            };

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendLine(identityError.Description);
                }

                throw new InvalidOperationException(sb.ToString().Trim());
            }

            if (role != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            return result.Succeeded;
        }

        /// <summary>
        /// Demotes the user as follows Guest &lt; Employee &lt; Admin.
        /// Returns true if the action is performed successfully.
        /// <para>Restrictions:
        ///     <para>  -The user cannot perform promotions on him self.</para>
        /// </para>
        /// </summary>
        /// <param name="userId">User for promotion.</param>
        /// <param name="currentUserId">The user who's performing the promotion</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> DemoteUser(string userId, Guid currentUserId)
        {
            var isUserIdValid = Guid.TryParse(userId, out var userGuid);
            if (!isUserIdValid)
            {
                throw new ArgumentException(InvalidUserIdFormat);
            }

            if (userGuid == currentUserId)
            {
                throw new InvalidOperationException(SelfPromotionError);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userGuid);
            if (user == null)
            {
                throw new InvalidOperationException(UserNotFound);
            }

            var currentUserRoles = await _userManager.GetRolesAsync(user);
            var role = currentUserRoles.FirstOrDefault();

            var result = role switch
            {
                "Admin" => await _userManager.AddToRoleAsync(user, "Employee"),
                "Employee" => await _userManager.RemoveFromRoleAsync(user, "Employee"),
                _ => IdentityResult.Failed(new[]
                {
                    new IdentityError(){Description = RoleUnderflow}
                })
            };

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendLine(identityError.Description);
                }

                throw new InvalidOperationException(sb.ToString().Trim());
            }

            if (role != "Employee")
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            return result.Succeeded;
        }


        /// <summary>
        /// Checks if a not persisted promotion is intersecting with other promotions.
        /// </summary>
        /// <param name="promotion">Promotion for validation.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task<bool> IsPreExistingPromotionInvalid(Promotion promotion)
        {
            bool result = true;

            if (promotion.StartDate > promotion.EndDate)
            {
                return result;
            }

            var authorId = promotion.AuthorPromotions
                .Select(ap => ap.AuthorId)
                .FirstOrDefault();

            var genreId = promotion.GenrePromotions
                .Select(gp => gp.GenreId)
                .FirstOrDefault();

            var canApplyPromotion = await _adminRepository
                .AllReadonly<Book>()
                .Where(b => b.AuthorId == authorId || b.GenreId == genreId)
                .CountAsync() > 0;

            if (!canApplyPromotion)
            {
                throw new InvalidOperationException(NoPromotionalAssortment);
            }

            var start = promotion.StartDate;
            var end = promotion.EndDate;

            result = await _adminRepository
                .AllReadonly<Book>()
                .Where(b => b.AuthorId == authorId || b.GenreId == genreId)
                .AnyAsync(b =>
                    b.Genre.GenrePromotions.Any(gp =>
                        (gp.Promotion.StartDate <= start && start <= gp.Promotion.EndDate) ||
                        (gp.Promotion.StartDate <= end && end <= gp.Promotion.EndDate)) ||
                    b.Author.AuthorPromotions.Any(ap =>
                        (ap.Promotion.StartDate <= start && start <= ap.Promotion.EndDate) ||
                        (ap.Promotion.StartDate <= end && end <= ap.Promotion.EndDate)));


            return result;
        }

        /// <summary>
        /// Checks if an existing promotion is intersecting with other promotions.
        /// </summary>
        /// <param name="promotion">Promotion for validation.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task<bool> IsExistingPromotionInvalid(Promotion promotion)
        {
            if (promotion.StartDate > promotion.EndDate)
            {
                return true;
            }

            var authorId = promotion.AuthorPromotions.Select(ap => ap.Author.Id).FirstOrDefault();
            var genreId = promotion.GenrePromotions.Select(gp => gp.Genre.Id).FirstOrDefault();

            var canApplyPromotion = await _adminRepository
                .AllReadonly<Book>()
                .Where(b => b.AuthorId == authorId || b.GenreId == genreId)
                .CountAsync() > 0;

            if (!canApplyPromotion)
            {
                throw new InvalidOperationException(NoPromotionalAssortment);
            }

            var promotionId = promotion.Id;
            var start = promotion.StartDate;
            var end = promotion.EndDate;

            bool result = await _adminRepository
                .AllReadonly<Book>()
                .Where(b => b.AuthorId == authorId || b.GenreId == genreId)
                .AnyAsync(b =>
                    b.Genre.GenrePromotions.Any(gp =>
                        gp.PromotionId != promotionId &&
                        ((gp.Promotion.StartDate <= start && start <= gp.Promotion.EndDate) ||
                         (gp.Promotion.StartDate <= end && end <= gp.Promotion.EndDate))) ||
                    b.Author.AuthorPromotions.Any(ap =>
                        ap.PromotionId != promotionId &&
                        ((ap.Promotion.StartDate <= start && start <= ap.Promotion.EndDate) ||
                         (ap.Promotion.StartDate <= end && end <= ap.Promotion.EndDate))));

            return result;
        }

        /// <summary>
        /// Gets the promotion discount percent if there's an existing promotion otherwise it returns 0.
        /// </summary>
        /// <param name="repository">Repository that holds the promotion.</param>
        /// <param name="genreId">Book's genre id.</param>
        /// <param name="authorId">Book's author id</param>
        /// <returns>Task&lt;decimal&gt;</returns>
        private static async Task<decimal> GetPromotion(IRepository repository, int genreId, int authorId)
        {
            var promotion = await repository
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
