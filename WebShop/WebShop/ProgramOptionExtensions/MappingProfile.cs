namespace WebShop.App.ProgramOptionExtensions
{
    using AutoMapper;
    using WebShop.Core.Models.BookShop;
    using WebShop.Services.Models.BookShop;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Book, ItemCard>();
            this.CreateMap<Genre, GenreCategoryIcon>();
        }
    }
}
