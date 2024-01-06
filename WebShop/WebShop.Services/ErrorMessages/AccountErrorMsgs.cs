namespace WebShop.Services.ErrorMessages
{
    public static class AccountErrorMsgs
    {
        public static class RegisterErrors
        {
            public const string FirstNameRequired = "The first name field is required.";

            public const string LastNameRequired = "The last name field is required.";

            public const string EmailRequired = "The email field is required.";

            public const string PasswordRequired = "The password field is required.";

            public const string ConfirmPasswordRequired = "The confirm password field is required.";

            public const string PhoneNumberRequired = "The phone number field is required.";


            public const string FirstNameLength = "First name must be between {2} and {1} characters long.";

            public const string LastNameLength = "Last name must be between {2} and {1} characters long.";

            public const string UsernameLength = "Username must be between {2} and {1} characters long.";

            public const string UserNameFormat = "Username can only have letter and digits.";
        }

        public static class LoginErrors
        {
            public const string UserNotFound = "Login failed! Wrong email or password was entered.";
        }
    }
}
