using AutoMapper;
using WebShop.Core.Models.BookShop;
using WebShop.Services.Models.Administration;
using WebShop.Services.Models.BookShop;

namespace WebShop.App.BuilderConfigurationExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Genre, GenreCategoryIcon>();
            this.CreateMap<Genre, GenreListItem>();
            this.CreateMap<Author, AuthorListItem>();

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
