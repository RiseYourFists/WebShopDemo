namespace WebShop.App.Areas.Administration.Models
{
    using WebShop.Services.Models.Administration;
    public class PromotionManageModel
    {
        public string SearchTerm { get; set; } = string.Empty;

        public List<PromotionListItem> Promotions { get; set; } = new();
    }
}
