using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.BookConstants;
    using static ValidationConstants.BookShopValidation.Shared;

    [JsonObject]
    [Comment("Book table")]
    public class Book
    {
        [Comment("Key identifier")]
        [JsonProperty("BookId")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLen)]
        [Comment("Book title")]
        [JsonProperty]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLen)]
        [Comment("Book description")]
        [JsonProperty]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Book's non-promotional price")]
        [JsonProperty]
        public decimal BasePrice { get; set; }

        [Required]
        [Comment("Book's price with or without promotion")]
        [JsonProperty]
        public decimal CurrentPrice { get; set; }

        [Required]
        [Comment("Genre key identifier")]
        [JsonProperty]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;

        [Required]
        [Comment("Author key identifier")]
        [JsonProperty]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;

        public virtual ICollection<PlacedOrderBook> PlacedOrderBooks { get; set; } = new HashSet<PlacedOrderBook>();
    }
}
