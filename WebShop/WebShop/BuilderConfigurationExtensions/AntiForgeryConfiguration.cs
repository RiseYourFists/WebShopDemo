namespace WebShop.App.BuilderConfigurationExtensions
{
    using Microsoft.AspNetCore.Antiforgery;
    public static class AntiForgeryConfiguration
    {
        public static AntiforgeryOptions ConfigureOptions(this AntiforgeryOptions options)
        {
            options.FormFieldName = "_anti_forgery_token";
            options.HeaderName = "X-XSRF-TOKEN";

            return options;
        }
    }
}
