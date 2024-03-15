namespace WebShop.App.Areas.Administration.Models
{
    public class HeaderNavigation
    {
        public HeaderNavigation(string buttonDescription, string action = "Books")
        {
            ButtonDescription = buttonDescription;
            Action = action;
        }
        public string Action { get; set; } = string.Empty;

        public string ButtonDescription { get; set; } = string.Empty;
    }
}
