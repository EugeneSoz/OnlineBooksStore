using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            return View(repository.Books);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            repository.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateBook(long key)
        {
            return View(repository.GetBook(key));
        }

        [HttpPost]
        public IActionResult UpdateBook(Book book)
        {
            repository.UpdateBook(book);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;

            return View(nameof(Index), repository.Books);
        }

        [HttpPost]
        public IActionResult UpdateAll(Book[] books)
        {
            repository.UpdateAll(books);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            repository.Delete(book);

            return RedirectToAction(nameof(Index));
        }
    }
}
