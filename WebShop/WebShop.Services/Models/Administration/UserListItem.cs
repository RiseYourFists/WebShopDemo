namespace WebShop.Services.Models.Administration
{
    public class UserListItem
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public string? Role { get; set; } = string.Empty;
    }
}
