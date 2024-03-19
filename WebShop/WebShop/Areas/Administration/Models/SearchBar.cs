namespace WebShop.App.Areas.Administration.Models
{
    public class SearchBar
    {
        public SearchBar(string action, string value)
        {
            Action = action;
            Value = value;
        }
        public string Action { get; set; }

        public string Value { get; set; }
    }
}
