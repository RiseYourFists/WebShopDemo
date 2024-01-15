using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.GenreConstants;

    [Comment("Genre table")]
    [JsonObject]
    public class Genre
    {
        [Key]
        [Comment("Key identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLen)]
        [Comment("Genre name")]
        [JsonProperty]
        public string Name { get; set; } = null!;

        public virtual ICollection<GenrePromotion> GenrePromotions { get; set; } = new HashSet<GenrePromotion>();
    }
}
