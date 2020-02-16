using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> Orders { get; }
        Order GetOrder(long key);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}