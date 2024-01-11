using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Core.Models.BookShop
{
    [Comment("Collection of all books that are ordered")]
    public class PlacedOrderBook
    {
        [Required]
        [Comment("Order Identifier")]
        public Guid PlacedOrderId { get; set; }

        [ForeignKey(nameof(PlacedOrderId))]
        public PlacedOrder PlacedOrder { get; set; } = null!;

        [Required]
        [Comment("Book Identifier")]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;
    }
}
