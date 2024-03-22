namespace WebShop.Services.Models.Administration
{
    using System.ComponentModel.DataAnnotations;
    using static ValidationConstants.AdministrationValidationConstants.BookValidation;
    using static ValidationConstants.AdministrationValidationConstants.SharedValidation;
    public class BookInfoModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(TitleMaxLen, MinimumLength = TitleMinLen, ErrorMessage = "The title must be between {2} and {1} characters long.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen, ErrorMessage = "The description must be between {2} and {1} characters long.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Base Price")]
        [Required(ErrorMessage = "Base price is required.")]
        [Range(BasePriceMinRange, BasePriceMaxRange, ErrorMessage = "The price must be between {1} and {2}.")]
        public decimal BasePrice { get; set; }

        [Display(Name = "Stock Quantity")]
        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(StockQuantityMinRange, StockQuantityMaxRange, ErrorMessage = "The stock quantity must be between {1} and {2}.")]
        public int StockQuantity { get; set; }

        [Display(Name = "Book Cover")]
        [Required(ErrorMessage = "Book cover is required.")]
        [StringLength(UrlMaxLen, MinimumLength = UrlMinLen, ErrorMessage = "The URL must be {2} and {1} characters long.")]
        public string BookCover { get; set; } = string.Empty;

        public int GenreId { get; set; }

        public int AuthorId { get; set; }

        public List<SelectionItemModel> Genres { get; set; } = new();

        public List<SelectionItemModel> Authors { get; set; } = new();
    }
}
