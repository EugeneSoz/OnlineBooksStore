using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IBooksRepository booksRepository, IOrdersRepository ordersRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        }

        public IActionResult Index() => View(_ordersRepository.Orders);

        public IActionResult EditOrder(long id)
        {
            var books = _booksRepository.Books;
            var order = id == 0 ? new Order() : _ordersRepository.GetOrder(id);
            var linesMap  = order.Lines?.ToDictionary(l => l.BookId) ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = books.Select(book => linesMap.ContainsKey(book.Id)
                ? linesMap[book.Id]
                : new OrderLine { Book = book, BookId = book.Id, Quantity = 0 });

            return View(order);
        }

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            order.Lines = order.Lines.Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();
            if (order.Id == 0)
            {
                _ordersRepository.AddOrder(order);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            _ordersRepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}