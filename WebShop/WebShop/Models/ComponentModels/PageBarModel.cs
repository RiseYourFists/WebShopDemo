namespace WebShop.App.Models.ComponentModels
{
    public class PageBarModel
    {
        public PageBarModel(
            int currentPage,
            int lastPage,
            Dictionary<string, object?> parameters,
            string action,
            string controller = "",
            string area = "")
        {
            CurrentPage = currentPage;
            LastPage = lastPage;
            Parameters = parameters;
            Action = action;
            Controller = controller;
            Area = area;
        }
        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public Dictionary<string, object?> Parameters { get; set; }
    }
}
