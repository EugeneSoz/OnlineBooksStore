using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Models;

namespace OnlineBooksStore.App.WebApi.Areas.Orders
{
    [Route("api/orders")]
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    [AutoValidateAntiforgeryToken]
    public class OrderController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public OrderController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Order>> GetOrdersAsync()
        {
            List<Order> orders = await _context.Orders
                .Include(o => o.Goods)
                .Include(o => o.Payment)
                .ToListAsync();

            return orders;
        }

        [HttpPost("{id}")]
        public async Task MarkShipped(long id)
        {
            Order order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Shipped = true;
                await _context.SaveChangesAsync();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderId = 0;
                order.Shipped = false;
                //не доверяем информации о сумме заказа, присланного с клиента
                order.Payment.Total = GetPrice(order.Goods);
                ProcessPayment(order.Payment);
                if (order.Payment.AuthCode != null)
                {
                    _context.Add(order);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        orderId = order.OrderId,
                        authCode = order.Payment.AuthCode,
                        amount = order.Payment.Total
                    });
                }
                else
                {
                    return BadRequest("Платёж отклонён");
                }
            }
            return BadRequest(ModelState);
        }

        private decimal GetPrice(List<OrderLine> lines)
        {
            //получить id всех книг в заказе
            IEnumerable<long> ids = lines.Select(l => l.ProductId);

            return _context.Books
                .Where(b => ids.Contains(b.Id))
                .Select(b => lines.First(l => l.ProductId == b.Id).Quantity * b.Price)
            .Sum();
        }

        private void ProcessPayment(Payment payment)
        {
            payment.AuthCode = "12345";
        }
    }
}
