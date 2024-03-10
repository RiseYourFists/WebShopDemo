namespace WebShop.Core.Contracts
{
    public interface IUserIdentity<T>
    {
        T Id { get; set; }
    }
}
