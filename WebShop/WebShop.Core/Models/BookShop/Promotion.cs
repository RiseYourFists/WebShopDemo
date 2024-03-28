using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.PromotionConstants;

    [JsonObject]
    [Comment("Promotion table")]
    public class Promotion
    {
        [Key]
        [JsonProperty("id")]
        [Comment("Id identifier")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("name")]
        [MaxLength(NameMaxLen)]
        [Comment("Promotion Name")]
        public string Name { get; set; } = null!;

        [Required]
        [JsonProperty("discountPercent")]
        [Comment("Discount percent")]
        public double DiscountPercent { get; set; }

        [Required]
        [JsonProperty("startDate")]
        [Comment("Start of promotion date")]
        public DateTime StartDate { get; set; }

        [Required]
        [JsonProperty("endDate")]
        [Comment("End of promotion date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<GenrePromotion> GenrePromotions { get; set; } = new HashSet<GenrePromotion>();

        public virtual ICollection<AuthorPromotion> AuthorPromotions { get; set; } = new HashSet<AuthorPromotion>();
    }
}
