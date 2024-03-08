using System.ComponentModel;

namespace WebShop.Core.Models.BookShop
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

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

        [NotMapped]
        public decimal CurrentPrice { get; set; }

        [Required]
        [Comment("Total quantity available")]
        [JsonProperty]
        public int StockQuantity { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        [Comment("Url for book cover")]
        [JsonProperty]
        public string BookCover { get; set; } = null!;

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
