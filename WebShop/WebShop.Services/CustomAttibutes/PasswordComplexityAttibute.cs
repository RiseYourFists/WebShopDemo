using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebShop.Core.Models.Identity;

namespace WebShop.Services.CustomAttibutes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public new string ErrorMessage { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value.ToString();
            var serviceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create a temporary user (doesn't need to be persisted)
            var tempUser = new ApplicationUser();

            // Use Identity's password validator to check complexity
            var passwordValidator = (IPasswordValidator<ApplicationUser>)validationContext
                .GetService(typeof(IPasswordValidator<ApplicationUser>));

            var validationResult = passwordValidator.ValidateAsync(userManager, tempUser, password).Result;

            if (validationResult.Succeeded)
            {
                return ValidationResult.Success;
            }

            var sb = new StringBuilder();
            foreach (var validationResultError in validationResult.Errors)
            {
                sb.AppendLine(validationResultError.Description);
            }

            ErrorMessage = sb.ToString();

            return new ValidationResult(ErrorMessage);
        }
    }
}
