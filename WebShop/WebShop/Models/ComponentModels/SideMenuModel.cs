namespace WebShop.App.Models.ComponentModels
{
    public class SideMenuModel
    {
        public SideMenuModel(string itemDescription, string area, string controller, string action, string classIcon, bool requireAuthentication)
        {
            ItemDescription = itemDescription;
            Area = area;
            Controller = controller;
            Action = action;
            ClassIcon = classIcon;
            RequireAuthentication = requireAuthentication;
        }

        public string ItemDescription { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string ClassIcon { get; set; }

        public bool RequireAuthentication { get; set; }
    }
}
