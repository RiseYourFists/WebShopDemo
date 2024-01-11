using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.AuthorConstants;

    [Comment("Author table")]
    public class Author
    {
        [Key]
        [Comment("Key identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLen)]
        [Comment("Author pseudo-name")]
        public string Name { get; set; } = null!;

        public virtual ICollection<AuthorPromotion> AuthorPromotions { get; set; } = new HashSet<AuthorPromotion>();
    }
}
