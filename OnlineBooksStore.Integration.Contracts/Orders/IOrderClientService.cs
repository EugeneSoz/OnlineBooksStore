using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Orders;

namespace OnlineBooksStore.Integration.Contracts.Orders
{
    public interface IOrderClientService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task MarkShippedAsync(OrderIdQuery query);
        Task CreateOrderAsync(CreateOrderCommand command);
    }
}