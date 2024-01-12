using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Core.Models.BookShop
{
    using static Core.ValidationConstants.BookShopValidation.Photo;

    [Comment("Photo Container")]
    public class Photo
    {
        [Key]
        [Comment("Key Identifier")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLen)]
        [Comment("Photo name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(PhotoMaxSize)]
        [Comment("Photo data")]
        public byte[] Bytes { get; set; } = null!;

        [Required]
        [MaxLength(FormatMaxLen)]
        [Comment("Photo extension")]
        public string FileExtension { get; set; } = null!;


        [Required]
        [Comment("Photo size")]
        public long Size { get; set; }

        [Required]
        [Comment("Book key identifier")]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;
    }
}
