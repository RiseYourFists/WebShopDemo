namespace WebShop.Services.Models.Administration
{
    using Enumerations;
    using static ValidationConstants.AdministrationValidationConstants.PromotionValidation;
    using System.ComponentModel.DataAnnotations;

    public class PromotionEditorModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Promotion name")]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen, ErrorMessage = "Promotion name must be between {2} and {1} characters long.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Discount percent")]
        [Range(DiscountMinRange, DiscountMaxRange, ErrorMessage = "The discount must be between {1} and {2} percent.")]
        public double DiscountPercent { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public int? AuthorId { get; set; }

        public int? GenreId { get; set; }

        public EditAction Action { get; set; }

        public List<SelectionItemModel> Genres { get; set; } = new();

        public List<SelectionItemModel> Authors { get; set; } = new();
    }
}
