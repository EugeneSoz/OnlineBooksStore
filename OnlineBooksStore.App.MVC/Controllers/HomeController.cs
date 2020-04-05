using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksRepository _booksRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IPublishersRepository _publishersRepository;

        public HomeController(
            IBooksRepository booksRepository, 
            ICategoriesRepository categoriesRepository, 
            IPublishersRepository publishersRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            _publishersRepository = publishersRepository ?? throw new ArgumentNullException(nameof(publishersRepository));
        }

        public IActionResult Index(QueryOptions options)
        {
            return View(_booksRepository.GetBooks(options));
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _booksRepository.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateBook(long key)
        {
            ViewBag.Categories = _categoriesRepository.Categories;
            ViewBag.Publishers = _publishersRepository.Publishers;

            return View(key == 0 ? new Book() : _booksRepository.GetBook(key));
        }

        [HttpPost]
        public IActionResult UpdateBook(Book book)
        {
            if (book.Id == 0)
            {
                _booksRepository.AddBook(book);
            }
            else
            {
                _booksRepository.UpdateBook(book);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;

            return View(nameof(Index), _booksRepository.Books);
        }

        [HttpPost]
        public IActionResult UpdateAll(Book[] books)
        {
            _booksRepository.UpdateAll(books);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _booksRepository.Delete(book);

            return RedirectToAction(nameof(Index));
        }
    }
}
