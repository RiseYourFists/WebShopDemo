using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Core.Models.BookShop
{
    [Comment("Promotion targeting genres")]
    public class GenrePromotion
    {
        [Required]
        [Comment("Promotion key identifier")]
        public int PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        public Promotion Promotion { get; set; } = null!;

        [Required]
        [Comment("Genre key identifier")]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;
    }
}
