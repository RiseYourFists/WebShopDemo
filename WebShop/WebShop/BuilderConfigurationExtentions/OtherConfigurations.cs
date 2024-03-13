﻿namespace WebShop.App.ProgramOptionExtensions
{
    using ModelBinders;

    public static class OtherConfigurations
    {
        public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {

            builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new FloatingPointBinderProvider());
                    options.ModelBinderProviders.Insert(0, new SanitizerProvider());
                });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;

            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            return builder;
        }
    }
}
