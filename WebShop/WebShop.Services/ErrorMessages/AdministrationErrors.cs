namespace WebShop.Services.ErrorMessages
{
    public static class AdministrationErrors
    {
        public const string InvalidBookId = "Invalid book id.";

        public const string InvalidGenreAuthorId = "Invalid genre/author id provided.";

        public const string InvalidGenreId = "Invalid genre id provided.";

        public const string InvalidAuthorId = "Invalid author id provided.";

        public const string InvalidPromotionId = "Invalid promotion id was provided.";

        public const string InvalidPromotionIdFormat = "Invalid promotion id format.";

        public const string GenreAuthorNotFound = "Genre/Author doesn't exist in this context.";

        public const string ExistingPromotions = "There are other promotions during the specified time";

        public const string InvalidUserIdFormat = "Invalid User id.";

        public const string SelfPromotionError = "Cannot change your own role.";

        public const string UserNotFound = "User not found";

        public const string RoleOverflow = "Cannot promote higher than Admin!";

        public const string RoleUnderflow = "Cannot demote lower than user!";
    }
}
