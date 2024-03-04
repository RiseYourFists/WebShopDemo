namespace WebShop.Core.ValidationConstants
{
    public static class BookShopValidation
    {
        public static class BookConstants
        {
            public const int TitleMaxLen = 35;

            public const int DescriptionMaxLen = 500;

        }

        public static class AuthorConstants
        {
            public const int NameMaxLen = 45;
        }

        public static class GenreConstants
        {
            public const int NameMaxLen = 45;
        }

        public static class PromotionConstants
        {
            public const int NameMaxLen = 50;
        }

        public static class PlacedOrderConstants
        {
            public const int CountryMaxLen = 100;
            public const int CityMaxLen = 100;
            public const int AddressMaxLen = 250;
        }

        public static class Shared
        {
            public const int UrlMaxLength = 2048;
        }
    }
}
