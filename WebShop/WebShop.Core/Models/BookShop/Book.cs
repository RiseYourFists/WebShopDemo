using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Core.Models.BookShop
{
    using static ValidationConstants.BookShopValidation.BookConstants;
    using static ValidationConstants.BookShopValidation.Shared;

    [Comment("Book table")]
    public class Book
    {
        [Comment("Key identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLen)]
        [Comment("Book title")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLen)]
        [Comment("Book description")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Book's non-promotional price")]
        public decimal BasePrice { get; set; }

        [Required]
        [Comment("Book's price with or without promotion")]
        public decimal CurrentPrice { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        [Comment("Book's cover photo")]
        public string CoverPhoto { get; set; } = null!;

        [Required]
        [Comment("Genre key identifier")]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;

        [Required]
        [Comment("Author key identifier")]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;

        public virtual ICollection<PlacedOrderBook> PlacedOrderBooks { get; set; } = new HashSet<PlacedOrderBook>();
    }
}
