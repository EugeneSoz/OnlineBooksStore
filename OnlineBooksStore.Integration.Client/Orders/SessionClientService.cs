using System;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.Domain.Contracts.Models.Orders;
using OnlineBooksStore.Integration.Contracts.Orders;

namespace OnlineBooksStore.Integration.Client.Orders
{
    public class SessionClientService : RestService, ISessionClientService
    {
        private const string Controller = "api/Session";
        public SessionClientService(HttpClient httpClient) : base(httpClient) { }

        public async Task<string> GetCartAsync()
        {
            return await GetJsonAsync<string>($"{Controller}/cart");
        }

        public async Task StoreCartAsync(OrderLine[] products)
        {
            await PostJsonAsync<bool>($"{Controller}/cart", products);
        }

        public async Task<string> GetCheckoutAsync()
        {
            return await GetJsonAsync<string>($"{Controller}/chechout");
        }

        public async Task StoreCheckoutAsync(CheckoutState data)
        {
            await PostJsonAsync<bool>($"{Controller}/checkout", data);
        }
    }
}