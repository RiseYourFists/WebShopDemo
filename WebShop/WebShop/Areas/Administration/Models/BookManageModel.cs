namespace WebShop.App.Areas.Administration.Models
{
    using WebShop.App.Models.ComponentModels;
    using WebShop.Services.Models.Administration;

    public class BookManageModel
    {
        public List<AuthorListItem> Authors { get; set; } = new();

        public List<GenreListItem> Genres { get; set; } = new();

        public List<BookListItem> Books { get; set; } = new();

        public List<DropdownListElement> GetAuthors()
        {
            var elements = new List<DropdownListElement>();

            this.Authors.ForEach(a =>
            {
                elements.Add(new()
                {
                    Action = "Books",
                    Area = "Administration",
                    Controller = "Manage",
                    ButtonClasses = "fas fa-user",
                    ButtonContent = a.Name,
                    Parameters = $"AuthorId={a.Id}"
                });
            });

            return elements;
        }
        
        public List<DropdownListElement> GetGenres()
        {
            var elements = new List<DropdownListElement>();

            this.Genres.ForEach(a =>
            {
                elements.Add(new()
                {
                    Action = "Books",
                    Area = "Administration",
                    Controller = "Manage",
                    ButtonClasses = "fas fa-book",
                    ButtonContent = a.Name,
                    Parameters = $"GenreId={a.Id}"
                });
            });

            return elements;
        }
    }

}
