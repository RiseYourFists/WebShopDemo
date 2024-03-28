using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebShop.Core.Models.BookShop
{
    [JsonObject]
    [Comment("Promotion targeting genres")]
    public class GenrePromotion
    {
        [Required]
        [JsonProperty("promotionId")]
        [Comment("Promotion key identifier")]
        public int PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        public Promotion Promotion { get; set; } = null!;

        [Required]
        [JsonProperty("genreId")]
        [Comment("Genre key identifier")]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;
    }
}
