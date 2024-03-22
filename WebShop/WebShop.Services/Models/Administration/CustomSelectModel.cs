namespace WebShop.Services.Models.Administration
{
    public class CustomSelectModel
    {
        public CustomSelectModel(
            string additionalArea,
            string additionalController,
            string additionalAction,
            string editArea,
            string editController,
            string editAction,
            string valueName,
            int checkedByValue,
            int tabIndex)
        {
            AdditionalArea = additionalArea;
            AdditionalController = additionalController;
            AdditionalAction = additionalAction;
            EditArea = editArea;
            EditController = editController;
            EditAction = editAction;
            ValueName = valueName;
            CheckedByValue = checkedByValue;
            TabIndex = tabIndex;
        }

        public CustomSelectModel(
            string valueName,
            int checkedByValue,
            int tabIndex,
            bool editOption = true,
            bool additionalOption = true,
            string additionalArea = "",
            string additionalController = "",
            string additionalAction = "",
            string editArea = "",
            string editController = "",
            string editAction = ""
            )
        {
            ValueName = valueName;
            CheckedByValue = checkedByValue;
            TabIndex = tabIndex;
            AdditionalOption = additionalOption;
            EditOption = editOption;
            AdditionalArea = additionalArea;
            AdditionalController = additionalController;
            AdditionalAction = additionalAction;
            EditArea = editArea;
            EditController = editController;
            EditAction = editAction;
        }

        public List<SelectionItemModel> Items { get; set; } = new();

        public string AdditionalArea { get; set; } = string.Empty;

        public string AdditionalController { get; set; } = string.Empty;

        public string AdditionalAction { get; set; } = string.Empty;

        public string EditArea { get; set; } = string.Empty;

        public string EditController { get; set; } = string.Empty;

        public string EditAction { get; set; } = string.Empty;

        public string ValueName { get; set; } = string.Empty;

        public int CheckedByValue { get; set; }

        public int TabIndex { get; set; }

        public bool EditOption { get; set; }

        public bool AdditionalOption { get; set; }
    }
}