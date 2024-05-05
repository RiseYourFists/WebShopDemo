namespace WebShop.Services.Contracts
{
    public interface IBookShopUtilityService
    {
        Task<string> GetSearchResults(string searchTerm);
    }
}
