namespace WebShop.App.Models.ComponentModels
{
    public class DropdownModel
    {
        public DropdownModel()
        {
            
        }

        public DropdownModel(
            string unfoldButtonContent,
            ICollection<DropdownListElement> elements
            )
        {
            UnfoldButtonContent = unfoldButtonContent;
            ElementList = elements.ToList();
        }
        public string? Heading { get; set; }

        public string UnfoldButtonContent { get; set; } = null!;

        public string? UnfoldButtonClasses { get; set; }

        public  List<DropdownListElement> ElementList { get; set; } = new();
    }
}
