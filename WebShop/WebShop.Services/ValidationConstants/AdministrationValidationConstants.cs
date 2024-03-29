﻿namespace WebShop.Services.ValidationConstants
{
    using static Core.ValidationConstants.BookShopValidation;
    public static class AdministrationValidationConstants
    {
        public static class BookValidation
        {
            public const int TitleMaxLen = BookConstants.TitleMaxLen;
            public const int TitleMinLen = 2;

            public const int DescriptionMaxLen = BookConstants.DescriptionMaxLen;
            public const int DescriptionMinLen = 2;

            public const double BasePriceMaxRange = double.MaxValue;
            public const double BasePriceMinRange = 1.00;

            public const int StockQuantityMaxRange = int.MaxValue;
            public const int StockQuantityMinRange = 0;
        }

        public static class GenreValidation
        {
            public const int NameMaxLen = GenreConstants.NameMaxLen;
            public const int NameMinLen = 3;
        }

        public static class AuthorValidation
        {
            public const int NameMaxLen = AuthorConstants.NameMaxLen;
            public const int NameMinLen = 2;
        }

        public static class PromotionValidation
        {
            public const int NameMaxLen = PromotionConstants.NameMaxLen;
            public const int NameMinLen = 5;

            public const double DiscountMaxRange = 90;
            public const double DiscountMinRange = 1;
        }

        public static class SharedValidation    
        {
            public const int UrlMaxLen = 2048;
            public const int UrlMinLen = 2;
        }
    }
}
