using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Persistence.EF
{
    public class OrdersRepository : BaseRepo<OrderEntity>, IOrdersRepository
    {
        public OrdersRepository(StoreDbContext ctx) : base(ctx) { }

        public IEnumerable<OrderEntity> GetOrders()
        {
            var query = GetEntities();
            var orders = query
                .Include(o => o.Lines)
                .Include(o => o.Payment);

            return orders.AsEnumerable();
        }

        public OrderEntity GetOrder(long key)
        {
            var query = GetEntities();
            var order = query.Include(o => o.Lines).FirstOrDefault(o => o.Id == key);

            return order;
        }

        public OrderEntity AddOrder(OrderEntity order)
        {
            return Add(order);
        }

        public bool UpdateOrder(OrderEntity order)
        {
            return Update(order);
        }

        public bool DeleteOrder(OrderEntity order)
        {
            return Delete(order);
        }
    }
}
