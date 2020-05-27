using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Orders;
using OnlineBooksStore.Integration.Contracts.Orders;

namespace OnlineBooksStore.Integration.Client.Orders
{
    public class OrderClientService : RestService, IOrderClientService
    {
        private const string Controller = "api/Order";
        public OrderClientService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await GetJsonAsync<IEnumerable<Order>>($"{Controller}");
        }

        public async Task MarkShippedAsync(OrderIdQuery query)
        {
            await GetJsonAsync<IEnumerable<Order>>($"{Controller}/{query.Id}");
        }

        public async Task CreateOrderAsync(CreateOrderCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}", command);
        }
    }
}