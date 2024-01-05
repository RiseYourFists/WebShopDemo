using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebShop.App.ProgramOptionExtensions
{
    public static class OtherConfigurations
    {
        public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.SlidingExpiration = true;

            });
            return builder;
        }
    }
}
