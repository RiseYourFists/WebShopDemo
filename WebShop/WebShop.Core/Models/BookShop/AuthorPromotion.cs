using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Core.Models.BookShop
{
    [Comment("Author targeted promotion")]
    public class AuthorPromotion
    {
        [Required]
        [Comment("Author key identifier")]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;

        [Required]
        [Comment("Promotion key identifier")]
        public int PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        public Promotion Promotion { get; set; } = null!;
    }
}
