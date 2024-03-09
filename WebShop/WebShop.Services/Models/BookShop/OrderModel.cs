namespace WebShop.Services.Models.BookShop
{
    using System.ComponentModel.DataAnnotations;
    using static ValidationConstants.BookShopConstants.OrderValidation;
    public class OrderModel
    {
        [Required]
        [StringLength(CountryMaxLen, MinimumLength = CountryMinLen)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(CityMaxLen, MinimumLength = CityMinLen)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(AddressMaxLen, MinimumLength = AddressMinLen)]
        public string Address { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
    }
}
