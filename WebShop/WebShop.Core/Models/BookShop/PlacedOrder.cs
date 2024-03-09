namespace WebShop.Core.Models.BookShop
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    using static ValidationConstants.BookShopValidation.PlacedOrderConstants;
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
        [Comment("Indication if the order is fulfilled. Default value is set to false.")]
        public bool IsFulfilled { get; set; } = false;

        [Required]
        [MaxLength(CountryMaxLen)]
        [Comment("Country of delivery.")]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(CityMaxLen)]
        [Comment("City of delivery.")]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLen)]
        [Comment("Address of delivery.")]
        public string Address { get; set; } = null!;

        [Required]
        [Comment("Indication of order stage")]
        public bool IsShipped { get; set; }

        public virtual ICollection<PlacedOrderBook> PlacedOrderBooks { get; set; } = new HashSet<PlacedOrderBook>();
    }
}
