namespace WebShop.Services.Models.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Enumerations;
    using static ValidationConstants.AdministrationValidationConstants.GenreValidation;
    using static ValidationConstants.AdministrationValidationConstants.SharedValidation;
    public class GenreEditorModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen, ErrorMessage = "Genre name must be between {2} and {1} characters long.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Icon URL")]
        [StringLength(UrlMaxLen, MinimumLength = UrlMinLen, ErrorMessage = "The URL must be between {2} and {1} characters long.")]
        public string IconLink { get; set; } = string.Empty;

        public GenreEditorAction Action { get; set; }
    }
}
