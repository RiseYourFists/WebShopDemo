using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WebShop.App.BuilderConfigurationExtensions
{
    public static class CorsConfiguration
    {
        public static CorsOptions ConfigureOptions(this CorsOptions options)
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:7295")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            return options;
        }
    }
}
