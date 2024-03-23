using WebShop.Services.Models.Administration.Enumerations;

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
            dynamic checkedByValue,
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
            AdditionalOption = true;
            EditOption = true;
        }

        public CustomSelectModel(
            string valueName,
            dynamic checkedByValue,
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

        public List<SelectionItemModel> Items { get; set; }

        public string AdditionalArea { get; set; } = string.Empty;

        public string AdditionalController { get; set; } = string.Empty;

        public string AdditionalAction { get; set; } = string.Empty;

        public string EditArea { get; set; } = string.Empty;

        public string EditController { get; set; } = string.Empty;

        public string EditAction { get; set; } = string.Empty;

        public string ValueName { get; set; } = string.Empty;

        public dynamic CheckedByValue { get; set; }

        public int TabIndex { get; set; }

        public bool EditOption { get; set; }

        public bool AdditionalOption { get; set; }
    }
}