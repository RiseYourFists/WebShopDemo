﻿namespace WebShop.Services.ServiceControllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Models.Administration;
    using WebShop.Core.Contracts;
    using WebShop.Core.Models.BookShop;

    public class AdministrationService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdministrationService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<List<GenreListItem>> GetGenres()
        {
            return await _adminRepository
                .AllReadonly<Genre>()
                .ProjectTo<GenreListItem>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<AuthorListItem>> GetAuthors()
        {
            return await _adminRepository
                .AllReadonly<Author>()
                .ProjectTo<AuthorListItem>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BookListItem>> GetBooks(string searchTerm, int? authorId, int? genreId)
        {
            var query = _adminRepository
                .AllReadonly<Book>();
                

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query
                    .Where(b => EF.Functions.Like(b.Title, $"%{searchTerm}%"));
            }

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }
            else if(genreId.HasValue)
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

        public async Task<BookInfoModel?> GetBookInfo(int id)
        {
            var book = await _adminRepository
                .All<Book>()
                .ProjectTo<BookInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }

        public async Task<bool> AnyGenre(int id)
        {
            return await _adminRepository
                .AllReadonly<Genre>()
                .AnyAsync(g => g.Id == id);
        }

        public async Task<bool> AnyAuthor(int id)
        {
            return await _adminRepository
                .AllReadonly<Author>()
                .AnyAsync(a => a.Id == id);
        }

        public async Task<Book?> GetBook(int id)
        {
            return await _adminRepository
                .All<Book>()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> EditBookInfo(BookInfoModel model)
        {
            var book = await GetBook(model.Id);

            if (book == null)
            {
                throw new InvalidOperationException("Invalid book id.");
            }

            var doesGenreExist = await AnyGenre(model.GenreId);
            var doesAuthorExist = await AnyAuthor(model.AuthorId);

            if (!doesAuthorExist || !doesGenreExist)
            {
                throw new InvalidOperationException("Invalid genre/author id provided.");
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

        private async Task<decimal> GetPromotion(IRepository repository, int genreId, int authorId)
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
