namespace WebShop.Services.Models.Administration
{
    public class UserPage
    {
        public Guid CurrentUserId { get; set; }

        public string SearchTerm { get; set; } = string.Empty;

        public List<UserListItem> Users { get; set; } = new();
    }
}
