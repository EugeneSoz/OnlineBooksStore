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
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublisherRepository _publisherRepository;

        public HomeController(
            IBookRepository bookRepository, 
            ICategoryRepository categoryRepository, 
            IPublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        }

        public IActionResult Index()
        {
            return View(_bookRepository.Books);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _bookRepository.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateBook(long key)
        {
            ViewBag.Categories = _categoryRepository.Categories;
            ViewBag.Publishers = _publisherRepository.Publishers;

            return View(key == 0 ? new Book() : _bookRepository.GetBook(key));
        }

        [HttpPost]
        public IActionResult UpdateBook(Book book)
        {
            if (book.Id == 0)
            {
                _bookRepository.AddBook(book);
            }
            else
            {
                _bookRepository.UpdateBook(book);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;

            return View(nameof(Index), _bookRepository.Books);
        }

        [HttpPost]
        public IActionResult UpdateAll(Book[] books)
        {
            _bookRepository.UpdateAll(books);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _bookRepository.Delete(book);

            return RedirectToAction(nameof(Index));
        }
    }
}
