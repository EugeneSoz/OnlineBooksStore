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
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Books);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _repository.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateBook(long key)
        {
            return View(key == 0 ? new Book() : _repository.GetBook(key));
        }

        [HttpPost]
        public IActionResult UpdateBook(Book book)
        {
            if (book.Id == 0)
            {
                _repository.AddBook(book);
            }
            else
            {
                _repository.UpdateBook(book);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;

            return View(nameof(Index), _repository.Books);
        }

        [HttpPost]
        public IActionResult UpdateAll(Book[] books)
        {
            _repository.UpdateAll(books);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _repository.Delete(book);

            return RedirectToAction(nameof(Index));
        }
    }
}
