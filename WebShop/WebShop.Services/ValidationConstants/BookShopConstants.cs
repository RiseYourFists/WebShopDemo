namespace WebShop.Services.ValidationConstants
{
    using static  Core.ValidationConstants.BookShopValidation;
    public static class BookShopConstants
    {
        public static class PhotoConstants
        {
            public const int PhotoMaxSize = Photo.PhotoMaxSize;
            public static readonly string[] AllowedExtensions = new[] { "png", "jpeg" };
        }
    }
}
