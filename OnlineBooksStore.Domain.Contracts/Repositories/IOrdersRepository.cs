using System.Collections.Generic;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IOrdersRepository
    {
        IEnumerable<OrderEntity> GetOrders();
        OrderEntity GetOrder(long key);
        OrderEntity AddOrder(OrderEntity order);
        bool UpdateOrder(OrderEntity order);
        bool DeleteOrder(OrderEntity order);
    }
}