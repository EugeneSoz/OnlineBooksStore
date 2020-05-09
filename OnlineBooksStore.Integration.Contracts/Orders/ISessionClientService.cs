using System.Threading.Tasks;
using OnlineBooksStore.Domain.Contracts.Models.Orders;

namespace OnlineBooksStore.Integration.Contracts.Orders
{
    public interface ISessionClientService
    {
        Task<string> GetCartAsync();
        Task StoreCartAsync(OrderLine[] products);
        Task<string> GetCheckoutAsync();
        Task StoreCheckoutAsync(CheckoutState data);
    }
}