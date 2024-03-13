namespace WebShop.App.MiddlewareConfigurationExtensions
{
    public static class EndpointsConfiguration
    {
        public static IEndpointRouteBuilder AddEndPoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "error",
                pattern: "/Error",
                defaults: new { controller = "Error", action = "Index" });

            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            return endpoints;
        }
    }
}
