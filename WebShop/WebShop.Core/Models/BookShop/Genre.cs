using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.GenreConstants;

    [Comment("Genre table")]
    public class Genre
    {
        [Key]
        [Comment("Key identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLen)]
        [Comment("Genre name")]
        public string Name { get; set; } = null!;

        public virtual ICollection<GenrePromotion> GenrePromotions { get; set; } = new HashSet<GenrePromotion>();
    }
}
