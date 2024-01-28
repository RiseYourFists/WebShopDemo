namespace WebShop.Core.Models.BookShop
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using static ValidationConstants.BookShopValidation.GenreConstants;
    using static ValidationConstants.BookShopValidation.Shared;

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

        [Required]
        [MaxLength(UrlMaxLength)]
        [JsonProperty]
        [Comment("Icon to display the category")]
        public string IconLink { get; set; } = null!;

        public virtual ICollection<GenrePromotion> GenrePromotions { get; set; } = new HashSet<GenrePromotion>();
    }
}
