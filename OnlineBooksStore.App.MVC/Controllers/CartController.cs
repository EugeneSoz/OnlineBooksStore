using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineBooksStore.App.MVC.Infrastructure;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        public CartController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        }
        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View(GetCart());
        }

        [HttpPost]
        public IActionResult AddToCart(Book book, string returnUrl)
        {
            SaveCart(GetCart().AddItem(book, 1));

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(long productId, string returnUrl)
        {
            SaveCart(GetCart().RemoveItem(productId));

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.Lines = GetCart().Selections.Select(s => new OrderLine
            {
                BookId = s.BookId,
                Quantity = s.Quantity
            }).ToArray();
            _ordersRepository.AddOrder(order);
            SaveCart(new Cart());

            return RedirectToAction(nameof(Completed));
        }
        public IActionResult Completed() => View();

        private Cart GetCart() => HttpContext.Session?.GetJson<Cart>("Cart") ?? new Cart();

        private void SaveCart(Cart cart) => HttpContext.Session.SetJson("Cart", cart);
    }
}