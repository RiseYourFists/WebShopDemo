using WebShop.Core.ValidationConstants;
using static WebShop.Core.ValidationConstants.IdentityConstants;

namespace WebShop.Services.ValidationConstants
{
    public static class AccountConstants
    {
        public static class RegisterConstants
        {
            public const int FirstNameMinLen = 2;

            public const int FirstNameMaxLen = FirstNameMaxLength;

            public const int LastNameMinLen = 2;

            public const int LastNameMaxLen = LastNameMaxLength;

            public const int UsernameMinLen = 5;

            public const int UsernameMaxLen = 256;

            public const string UsernameValidationFormat = @"^[\w\d]+$";
        }
    }
}
