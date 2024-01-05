using System.ComponentModel;

namespace WebShop.Services.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [PasswordPropertyText]
        public string Password { get; set; } = null!;
    }
}
