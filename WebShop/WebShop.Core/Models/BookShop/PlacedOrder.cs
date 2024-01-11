using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models.BookShop
{
    [Comment("Placed order")]
    public class PlacedOrder
    {
        [Key]
        [Comment("Key identifier")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Identity user id")]
        public Guid UserId { get; set; }

        [Required]
        [Comment("Date of order")]
        public DateTime DatePlaced { get; set; }

        [Comment("Date of the order fulfillment")]
        public DateTime? DateFulfilled { get; set; }

        [Required]
        [Comment("Total price for the order")]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<PlacedOrderBook> PlacedOrderBooks { get; set; } = new HashSet<PlacedOrderBook>();
    }
}
