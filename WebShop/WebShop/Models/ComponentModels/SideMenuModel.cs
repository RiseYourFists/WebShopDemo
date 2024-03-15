namespace WebShop.App.Models.ComponentModels
{
    public class SideMenuModel
    {
        public SideMenuModel(
            string itemDescription = "Element",
            string area = "",
            string controller = "",
            string action = "",
            string classIcon = "",
            bool requireAuthentication = false,
            int notifications = 0)
        {
            ItemDescription = itemDescription;
            Area = area;
            Controller = controller;
            Action = action;
            ClassIcon = classIcon;
            RequireAuthentication = requireAuthentication;
            AccessRoles = Array.Empty<string>();
            Notifications = notifications;
        }

        public SideMenuModel(
            string[] accessRoles,
            string itemDescription = "Element",
            string area = "",
            string controller = "",
            string action = "",
            string classIcon = "",
            int notifications = 0)
        {
            ItemDescription = itemDescription;
            Area = area;
            Controller = controller;
            Action = action;
            ClassIcon = classIcon;
            RequireAuthentication = true;
            AccessRoles = accessRoles;
            Notifications = notifications;
        }

        public string ItemDescription { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string ClassIcon { get; set; }

        public bool RequireAuthentication { get; private set; }

        public string[] AccessRoles { get; private set; }

        public int Notifications { get; set; }
    }
}
