namespace WebShop.Services.Models.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Enumerations;
    using static ValidationConstants.AdministrationValidationConstants.AuthorValidation;
    public class AuthorEditorModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen, ErrorMessage = "Name must be between {2} and {1} characters long.")]
        public string Name { get; set; } = string.Empty;

        public EditAction Action { get; set; }
    }
}
