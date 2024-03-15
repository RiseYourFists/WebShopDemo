namespace WebShop.App.Models.ComponentModels
{
    public class DropdownListElement
    {
        public DropdownListElement()
        {
            
        }

        public DropdownListElement(string area, string controller, string action, string buttonContent)
        {
            Area = area;
            Controller = controller;
            Action = action;
            ButtonContent = buttonContent;
            Parameters = null;
        }

        public DropdownListElement(string area, string controller, string action, string buttonContent, string parameters)
        {
            Area = area;
            Controller = controller;
            Action = action;
            ButtonContent = buttonContent;
            Parameters = parameters;
        }

        public string? Area { get; set; }

        public string Controller { get; set; } = null!;

        public string Action { get; set; } = null!;

        public string ButtonContent { get; set; } = null!;

        public string? ButtonClasses { get; set; }

        public string? Parameters { get; set; }
    }
}
