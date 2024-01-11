using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.PromotionConstants;

    [Comment("Promotion table")]
    public class Promotion
    {
        [Key]
        [Comment("Id identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLen)]
        [Comment("Promotion Name")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("Discount percent")]
        public double DiscountPercent { get; set; }

        [Required]
        [Comment("Start of promotion date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Comment("End of promotion date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<GenrePromotion> GenrePromotions { get; set; } = new HashSet<GenrePromotion>();

        public virtual ICollection<AuthorPromotion> AuthorPromotions { get; set; } = new HashSet<AuthorPromotion>();
    }
}
