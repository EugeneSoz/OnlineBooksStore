using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Integration.Contracts.Store;

namespace OnlineBooksStore.Integration.Client.Store
{
    public class AccountClientService : RestService, IAccountClientService
    {
        private const string Controller = "api/Account";
        public AccountClientService(HttpClient httpClient) : base(httpClient) { }

        public async Task LoginAsync(Login creds)
        {
            await PostJsonAsync<bool>($"{Controller}/login", creds);
        }

        public async Task LogoutAsync()
        {
            await PostJsonAsync<bool>($"{Controller}/logunt", null);
        }
    }
}