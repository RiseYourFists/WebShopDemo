namespace WebShop.App.ProgramOptionExtensions
{
    using Microsoft.AspNetCore.Identity;
    public static class IdentityConfiguration
    {
        public static IdentityOptions AddOptions(this IdentityOptions options, ConfigurationManager config)
        {
            var identityOptions = "IdentityOpt:";
            var passwordConfig = options.Password;

            passwordConfig.RequireDigit = config.GetValue<bool>(identityOptions + nameof(passwordConfig.RequireDigit));
            passwordConfig.RequireLowercase = config.GetValue<bool>(identityOptions + nameof(passwordConfig.RequireLowercase));
            passwordConfig.RequireUppercase = config.GetValue<bool>(identityOptions + nameof(passwordConfig.RequireUppercase));
            passwordConfig.RequireNonAlphanumeric = config.GetValue<bool>(identityOptions + nameof(passwordConfig.RequireNonAlphanumeric));

            passwordConfig.RequiredLength = config.GetValue<int>(identityOptions + nameof(passwordConfig.RequiredLength));
            passwordConfig.RequiredUniqueChars = config.GetValue<int>(identityOptions + nameof(passwordConfig.RequiredUniqueChars));

            return options;
        }
    }
}
