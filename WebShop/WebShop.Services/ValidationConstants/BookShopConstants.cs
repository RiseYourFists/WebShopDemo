namespace WebShop.Services.ValidationConstants
{
    using static  Core.ValidationConstants.BookShopValidation;
    public static class BookShopConstants
    {
        public static class OrderValidation
        {
            public const int CountryMaxLen = PlacedOrderConstants.CountryMaxLen;
            public const int CountryMinLen = 2;

            public const int CityMaxLen = PlacedOrderConstants.CityMaxLen;
            public const int CityMinLen = 2;

            public const int AddressMaxLen = PlacedOrderConstants.AddressMaxLen;
            public const int AddressMinLen = 2;
        }
    }
}
