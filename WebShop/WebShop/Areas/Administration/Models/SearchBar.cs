namespace WebShop.App.Areas.Administration.Models
{
    public class SearchBar
    {
        public SearchBar(string action)
        {
            Action = action;
        }
        public string Action { get; set; }
    }
}
