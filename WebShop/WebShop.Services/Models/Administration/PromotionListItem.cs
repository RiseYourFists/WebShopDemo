namespace WebShop.Services.Models.Administration
{
    public class PromotionListItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double DiscountPercent { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
