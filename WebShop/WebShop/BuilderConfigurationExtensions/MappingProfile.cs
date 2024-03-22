namespace WebShop.App.BuilderConfigurationExtensions
{
    using AutoMapper;
    using WebShop.Core.Models.BookShop;
    using WebShop.Services.Models.BookShop;
    using WebShop.Services.Models.Administration;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Genre, GenreCategoryIcon>();
            this.CreateMap<Genre, GenreListItem>();
            this.CreateMap<Author, AuthorListItem>();
            this.CreateMap<Genre, GenreEditorModel>();
            this.CreateMap<Author, AuthorEditorModel>();
            this.CreateMap<Promotion, PromotionListItem>();

            this.CreateMap<BookInfoModel, Book>()
                .ForMember(p => p.Id,
                    dest => dest.Ignore());

            this.CreateMap<AuthorEditorModel, Author>()
                .ForMember(p => p.Id,
                    dest => dest.Ignore());

            this.CreateMap<GenreEditorModel, Genre>()
                .ForMember(p => p.Id,
                    dest => dest.Ignore());

            this.CreateMap<Book, BookDetail>()
                .ForMember(p => p.Genre,
                    dest => dest.MapFrom(
                        opt => opt.Genre.Name))
                .ForMember(p => p.Author,
                    dest => dest.MapFrom(
                        opt => opt.Author.Name));

            this.CreateMap<Book, BookInfoModel>()
                .ForMember(p => p.BasePrice,
                    dest => dest.MapFrom(
                        opt => opt.BasePrice));
        }
    }
}
