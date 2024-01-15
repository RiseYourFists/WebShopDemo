using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.AuthorConstants;

    [Comment("Author table")]
    [JsonObject]
    public class Author
    {
        [Key]
        [Comment("Key identifier")]
        [JsonProperty]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLen)]
        [Comment("Author pseudo-name")]
        [JsonProperty]
        public string Name { get; set; } = null!;

        public virtual ICollection<AuthorPromotion> AuthorPromotions { get; set; } = new HashSet<AuthorPromotion>();
    }
}
