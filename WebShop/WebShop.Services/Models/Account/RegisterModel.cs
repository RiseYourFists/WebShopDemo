using System.ComponentModel.DataAnnotations;
using WebShop.Services.CustomAttributes;
using static WebShop.Services.ErrorMessages.AccountErrorMsgs.RegisterErrors;
using static WebShop.Services.ValidationConstants.AccountConstants.RegisterConstants;

namespace WebShop.Services.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = FirstNameRequired)]
        [StringLength(FirstNameMaxLen, MinimumLength = FirstNameMinLen, ErrorMessage = FirstNameLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = LastNameRequired)]
        [StringLength(LastNameMaxLen, MinimumLength = LastNameMinLen, ErrorMessage = LastNameLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(UsernameMaxLen, MinimumLength = UsernameMinLen, ErrorMessage = UsernameLength)]
        [RegularExpression(UsernameValidationFormat, ErrorMessage = UserNameFormat)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = EmailRequired)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = PasswordRequired)]
        [DataType(DataType.Password)]
        [PasswordComplexity]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = ConfirmPasswordRequired)]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = PhoneNumberRequired)]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
    }
}
