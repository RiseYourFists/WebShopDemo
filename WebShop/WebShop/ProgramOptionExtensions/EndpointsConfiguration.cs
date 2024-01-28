namespace WebShop.App.ProgramOptionExtensions
{
    public static class EndpointsConfiguration
    {
        public static IEndpointRouteBuilder AddEndPoints(this IEndpointRouteBuilder endpoints)
        {
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
