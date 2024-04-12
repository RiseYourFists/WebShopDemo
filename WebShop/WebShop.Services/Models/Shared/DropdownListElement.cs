namespace WebShop.Services.Models.Shared
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

        public DropdownListElement(string area, string controller, string action, string buttonContent, Dictionary<string, object?> parameters)
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

        public Dictionary<string, object?> Parameters { get; set; }
    }
}
