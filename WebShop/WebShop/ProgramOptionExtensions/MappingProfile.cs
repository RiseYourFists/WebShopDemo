namespace WebShop.App.ProgramOptionExtensions
{
    using AutoMapper;
    using WebShop.Core.Models.BookShop;
    using WebShop.Services.Models.BookShop;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Genre, GenreCategoryIcon>();

            this.CreateMap<Book, BookDetail>()
                .ForMember(p => p.Genre, 
                    dest => dest.MapFrom(
                        opt => opt.Genre.Name))
                .ForMember(p => p.Author,
                    dest => dest.MapFrom(
                        opt => opt.Author.Name));
        }
    }
}
