namespace WebShop.Services.Models.BookShop
{
    public class Invoice
    {
        public string CustomerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; } 
    }
}
